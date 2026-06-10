using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OfficeWorkTracker.Application; 
using OfficeWorkTracker.Application.Interfaces; 
using OfficeWorkTracker.Application.Service;
using OfficeWorkTracker.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Infrastructure.Repositories;
using OfficeWorkTracker.API.Middleware;

// Create the WebApplicationBuilder, reading configuration, env, and args
var builder = WebApplication.CreateBuilder(args);

// Use the DefaultConnection string from configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => // Register EF Core DbContext with DI
    options.UseSqlServer( // Configure the DbContext to use SQL Server
        builder.Configuration.GetConnectionString("DefaultConnection"))); 

builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register IUserRepository with scoped lifetime and concrete UserRepository
builder.Services.AddScoped<IUserService, UserService>(); // Register IUserService with scoped lifetime and concrete UserService
builder.Services.AddScoped<ITaskRespository, TaskRepository>(); // Register ITaskRespository with scoped lifetime and concrete TaskRepository
builder.Services.AddScoped<ITaskService, TaskService>(); // Register ITaskService with scoped lifetime and concrete TaskService
builder.Services.AddScoped<IDashboardService, Dashboards>(); // Register IDashboardService with scoped lifetime and concrete Dashboards
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Register minimal API endpoint metadata for OpenAPI/Swagger generation

// Register Swagger generator to produce OpenAPI documents
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", 
        new OpenApiInfo 
        { 
            Title = "OfficeWorkTracker API", Version = "v1" 
        }); // Define a Swagger document with title and version

    option.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="bearer"
                }
            },
            Array.Empty<string>()
        }
    });
}); 

builder.Services.AddScoped<IJwtTokenServices, JwtTokenService>(); // Register IJwtTokenServices with scoped lifetime and concrete JwtTokenService
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            Console.WriteLine("TOKEN RECEIVED:");
            Console.WriteLine(context.Token);

            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("AUTH FAILED:");
            Console.WriteLine(context.Exception.ToString());

            return Task.CompletedTask;
        },

        OnTokenValidated = context =>
        {
            Console.WriteLine("TOKEN VALIDATED SUCCESSFULLY");

            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true, //Who create the token

            ValidateAudience = true, //Who receive the token

            ValidateLifetime = true, //Check token is expired or not

            ValidateIssuerSigningKey = true, //Check signature in token is valid or not

            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Read the valid issuer from configuration (e.g., "Jwt:Issuer" key)

            ValidAudience = builder.Configuration["Jwt:Audience"], // Read the valid audience from configuration (e.g., "Jwt:Audience" key)

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]))
        };
});

builder.Services.AddAuthorization(); // Register authorization services to enable policy-based authorization

var app = builder.Build(); // Build the WebApplication (finalize middleware pipeline and service provider)

app.UseMiddleware<ExceptionMiddleware>(); // Register custom middleware for global exception handling

if (app.Environment.IsDevelopment()) // Check if the current environment is Development
{
    app.UseSwagger(); // Enable middleware to serve generated Swagger as JSON endpoint

    app.UseSwaggerUI(); // Enable middleware to serve Swagger UI (HTML/JS) at default endpoint
}

app.UseHttpsRedirection(); // Middleware that redirects HTTP requests to HTTPS

app.UseAuthentication(); // Authentication middleware that validates JWT tokens and sets user principal for the request

app.UseAuthorization(); // Authorization middleware that enforces policies for authenticated requests

app.MapControllers(); // Map attribute-routed controllers to endpoints

app.Run(); // Run the application and start listening for incoming HTTP requests

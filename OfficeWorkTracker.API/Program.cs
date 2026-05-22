using Microsoft.AspNetCore.Builder; // Provides WebApplication, middleware extension methods and builder types
using Microsoft.EntityFrameworkCore; // Provides DbContext and EF Core extension methods like UseSqlServer
using OfficeWorkTracker.Application; // Reference to application layer (for DI extensions or services, if any)
using OfficeWorkTracker.Application.Interfaces; // Contains application-level interfaces (e.g., IUserRepository)
using OfficeWorkTracker.Application.Service; // Contains concrete service implementations (e.g., UserService)
using OfficeWorkTracker.Infrastructure.Data; // Contains ApplicationDbContext and data-layer types

var builder = WebApplication.CreateBuilder(args); // Create the WebApplicationBuilder, reading configuration, env, and args
builder.Services.AddDbContext<ApplicationDbContext>(options => // Register EF Core DbContext with DI
    options.UseSqlServer( // Configure the DbContext to use SQL Server
        builder.Configuration.GetConnectionString("DefaultConnection"))); // Use the DefaultConnection string from configuration

builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register IUserRepository with scoped lifetime and concrete UserRepository
builder.Services.AddScoped<IUserService, UserService>(); // Register IUserService with scoped lifetime and concrete UserService
builder.Services.AddEndpointsApiExplorer(); // Register minimal API endpoint metadata for OpenAPI/Swagger generation
builder.Services.AddSwaggerGen(); // Register Swagger generator to produce OpenAPI documents
builder.Services.AddControllers(); // Register MVC controllers with DI so controller endpoints are available
builder.Services.AddScoped<IJwtTokenServices, JwtTokenService>(); // Register IJwtTokenServices with scoped lifetime and concrete JwtTokenService
var app = builder.Build(); // Build the WebApplication (finalize middleware pipeline and service provider)

if (app.Environment.IsDevelopment()) // Check if the current environment is Development
{
    app.UseSwagger(); // Enable middleware to serve generated Swagger as JSON endpoint

    app.UseSwaggerUI(); // Enable middleware to serve Swagger UI (HTML/JS) at default endpoint
}

app.UseHttpsRedirection(); // Middleware that redirects HTTP requests to HTTPS

app.UseAuthorization(); // Authorization middleware that enforces policies for authenticated requests

app.MapControllers(); // Map attribute-routed controllers to endpoints

app.Run(); // Run the application and start listening for incoming HTTP requests
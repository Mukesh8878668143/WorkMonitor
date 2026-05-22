using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OfficeWorkTracker.Application.Service
{
    /// <summary>
    /// Important: Responsible for creating JWTs for authenticated users.
    /// - Reads signing configuration from IConfiguration keys: "Jwt:Key", "Jwt:Issuer", "Jwt:Audience".
    /// - Produces a signed token valid for 1 hour by default.
    /// </summary>
    public class JwtTokenService : IJwtTokenServices
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Important: IConfiguration is injected so secrets and values remain configurable (avoids hard-coding).
        /// Ensure required keys exist in configuration or token generation will fail at runtime.
        /// </summary>
        public JwtTokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// Important: Creates a JWT for the provided <see cref="User"/>.
        /// Flow:
        /// 1. Build claims representing the user identity and role.
        /// 2. Create a symmetric signing key from configured secret.
        /// 3. Create signing credentials using HMAC-SHA256.
        /// 4. Build the JwtSecurityToken with issuer, audience, claims, expiry and signing credentials.
        /// 5. Return the serialized token string.
        /// 
        /// Security notes:
        /// - Keep "Jwt:Key" secret and sufficiently long/complex (recommend >= 256 bits).
        /// - Consider rotating keys and using asymmetric keys for higher security.
        /// - Validate configuration values at startup to fail fast if keys are missing.
        /// </summary>
        /// <param name="user">The user for whom the token is generated. Email and Role must be present.</param>
        /// <returns>Serialized JWT as string.</returns>
        public string GenerateToken(User user)
        {
            // Important: Claims establish identity and roles used by authorization policies later.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Important: Read the signing key from configuration.
            // If Jwt:Key is missing or empty, Encoding.GetBytes will produce an invalid key and token creation will fail.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Important: Use HMAC-SHA256 to sign tokens. SigningCredentials ties the key to the algorithm.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Important: Token metadata - issuer and audience are used by token validation parameters when authenticating requests.
            // If you require longer or shorter validity, adjust the expires value or make it configurable.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            // Important: Serialize the token to a compact string to be returned to clients.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

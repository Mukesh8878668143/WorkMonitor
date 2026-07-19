using OfficeWorkTracker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Service
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public string GenerateRefreshToken()
        {
            var rendomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(rendomBytes);
        }
    }
}

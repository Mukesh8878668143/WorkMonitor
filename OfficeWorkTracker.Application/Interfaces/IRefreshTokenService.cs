using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Application.DTOs.Auth;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        //Task<UserRefereshToken> RefreshTokenAsync(RefreshTokenDto dto);
    }
}

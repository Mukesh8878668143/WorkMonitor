using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface IUserRefreshTokenRepository
    {
        Task AddAsync(UserRefereshToken refreshToken);

        Task<UserRefereshToken> GetByTokenAsync(string refreshToken);
        Task UpdateAsync(UserRefereshToken refereshToken);
    }
}

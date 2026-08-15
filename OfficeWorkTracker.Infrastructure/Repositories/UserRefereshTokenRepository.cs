using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Infrastructure.Data;

namespace OfficeWorkTracker.Infrastructure.Repositories
{
    

    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly ApplicationDbContext _Context;

        public UserRefreshTokenRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task AddAsync(UserRefereshToken refereshToken)
        {
            await _Context.UserRefereshTokens.AddAsync(refereshToken);
            await _Context.SaveChangesAsync();
        }

        public async Task<UserRefereshToken> GetByTokenAsync(string refreshToken)
        {
            if (refreshToken != null)
            {
                return await _Context.UserRefereshTokens
                    .Include(x => x.user)
                    .FirstOrDefaultAsync(rt => rt.RefereshToken == refreshToken)!;
            }
            return null!;
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(UserRefereshToken refereshToken)
        {
            _Context.UserRefereshTokens.Update(refereshToken);
            await _Context.SaveChangesAsync();
        }
    }
}

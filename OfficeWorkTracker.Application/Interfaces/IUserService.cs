using OfficeWorkTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
        Task<List<UserResponseDto>> GetAllUserAsync();
        Task<UserResponseDto> GetUserByIdAsync(Guid id);
        Task DeleteUserAsync(Guid id);
    }
}

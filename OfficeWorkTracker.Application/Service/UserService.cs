using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;

namespace OfficeWorkTracker.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = "Employees",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserResponseDto>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

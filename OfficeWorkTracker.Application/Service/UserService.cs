using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using OfficeWorkTracker.Application.DTOs.Auth;
namespace OfficeWorkTracker.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenServices _jwtTokenService;
        public UserService(IUserRepository userRepository, IJwtTokenServices jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                
                FullName = dto.FullName,
                Email = dto.Email,
                Role = "Employees",
                CreatedAt = DateTime.UtcNow
            };

            var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;

            await _userRepository.CreateAsync(user);
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                await _userRepository.DeleteAsync(user.Result);
                return true;
            }
        }

        public async Task<List<UserResponseDto>> GetAllUserAsync()
        {
            var user = await _userRepository.GetAllAsync();
            return user.Select(user => new UserResponseDto
            {
                //Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            }).ToList();
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserResponseDto
                {
                    //Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                };
            }
        }

        public async Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                user.FullName = dto.FullName;
                user.Role = dto.Role;
                await _userRepository.UpdateAsync(user);
                return new UserResponseDto
                {
                    //Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                };
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return null;
            }
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var token = _jwtTokenService.GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}

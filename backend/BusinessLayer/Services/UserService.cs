using BusinessLayer.IServices;
using DataAccessLayer.IRepository;
using DTOs.DTOModels;
using DTOs.Mapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserDTOModel> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<UserDTOModel> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserDTOModel>> GetAllUsersAsync()
        {
            var usersEF = await _userRepository.GetAllUsersAsync();
            var usersDTO = usersEF.Select(u => UserMapper.ToDTO(u));
            return usersDTO;
        }

        public async Task<UserDTOModel> SignUpAsync(UserDTOModel user)
        {
            var userEF = UserMapper.ToEFModel(user);
            userEF.Password = _passwordHasher.HashPassword(user, user.Password);
            var returnUser = await _userRepository.SignUpAsync(userEF);
            return UserMapper.ToDTO(returnUser);
        }

        public async Task<UserDTOModel?> LogInAsync(string email, string password)
        {
            var userEF = await _userRepository.GetByEmailAsync(email);
            if (userEF == null)
            {
                return null;
            }
            var user = UserMapper.ToDTO(userEF);
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }
            return null;
        }

        public async Task<UserDTOModel> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            return UserMapper.ToDTO(user);
        }
    }
}

using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTOModel>> GetAllUsersAsync();
        public Task<UserDTOModel> GetUserByIdAsync(Guid userId);
        public Task<UserDTOModel> SignUpAsync(UserDTOModel user);
        public Task<UserDTOModel> LogInAsync(string email, string password);
    }
}

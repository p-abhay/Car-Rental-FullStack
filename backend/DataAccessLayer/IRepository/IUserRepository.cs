using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserEFModel>> GetAllUsersAsync();
        public Task<UserEFModel> GetUserByIdAsync(Guid id);
        public Task<UserEFModel> SignUpAsync(UserEFModel user);
        public Task<UserEFModel> GetByEmailAsync(string email);
    }
}

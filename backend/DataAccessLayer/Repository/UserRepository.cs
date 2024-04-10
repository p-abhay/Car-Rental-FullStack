using DataAccessLayer.DBContext;
using DataAccessLayer.EFModels;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _carRentalDbContext;
        public UserRepository(CarRentalDbContext carRentalDbContext)
        {
            _carRentalDbContext = carRentalDbContext;
        }

        public async Task<IEnumerable<UserEFModel>> GetAllUsersAsync()
        {
            return await _carRentalDbContext.Users.ToListAsync();
        }

        public async Task<UserEFModel?> GetByEmailAsync(string email)
        {
            var user = await _carRentalDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEFModel> GetUserByIdAsync(Guid id)
        {
            var user = await _carRentalDbContext.Users.Where(x=>x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEFModel> SignUpAsync(UserEFModel user)
        {
            await _carRentalDbContext.Users.AddAsync(user);
            await _carRentalDbContext.SaveChangesAsync();
            return user;
        }
    }
}

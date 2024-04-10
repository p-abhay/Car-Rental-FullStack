using DataAccessLayer.DBContext;
using DataAccessLayer.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SeedUser
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CarRentalDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CarRentalDbContext>>()))
            {
                // Ensure the database is created
                context.Database.EnsureCreated();

                // Look for any users.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(//Admin@24
                    new UserEFModel
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Admin",
                        Email = "admin@admin.com",
                        PhoneNumber = "9876543210",
                        Password = "AQAAAAEAACcQAAAAEOIsoNKx/KiIAlNMnm1sZxxpLfVSu/58ClIu6KuwdNOuXrufXk7BC47etGilFseDuA==",
                        IsAdmin = true
                    },

                    new UserEFModel
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Abhay Sankhwar",
                        Email = "abhay.s@mail.com",
                        PhoneNumber = "9876543219",
                        Password = "AQAAAAEAACcQAAAAEM2ghMT/y6nuSZDbCkPfff7I1JHNnoSyVerVzhCl2+Xkf3rcGaL/8B7S277fxp6sBA==",
                        IsAdmin = false
                    },

                    new UserEFModel
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Ethan",
                        Email = "ethan@mail.com",
                        PhoneNumber = "9876543219",
                        Password = "AQAAAAEAACcQAAAAEM2ghMT/y6nuSZDbCkPfff7I1JHNnoSyVerVzhCl2+Xkf3rcGaL/8B7S277fxp6sBA==",
                        IsAdmin = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

using DataAccessLayer.EFModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DBContext
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarEFModel>()
                .Property(c => c.RentalPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<CarBookingEFModel>()
                .Property(c => c.TotalPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<CarBookingEFModel>()
                .Property(e => e.Status)
                .HasConversion<int>();
        }

        public DbSet<UserEFModel> Users { get; set; }
        public DbSet<CarEFModel> Cars { get; set; }

        public DbSet<CarBookingEFModel> CarBookings { get; set; }
    }
}

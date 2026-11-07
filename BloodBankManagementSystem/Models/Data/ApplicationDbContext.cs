using BloodBankManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed one stock row for each blood group with 0 units
            foreach (BloodGroup bg in Enum.GetValues(typeof(BloodGroup)))
            {
                modelBuilder.Entity<BloodStock>().HasData(new BloodStock
                {
                    Id = (int)bg,
                    BloodGroup = bg,
                    Units = 0,
                    LastUpdated = new DateTime(2026, 1, 1)
                });
            }
        }
    }
}

using LaunDrive.Models;
using Microsoft.EntityFrameworkCore;

namespace LaunDrive.Data
{
    public class LaunDriveDbContext : DbContext
    {
        public LaunDriveDbContext(
            DbContextOptions<LaunDriveDbContext> options)
            : base(options)
        {
        }

        public DbSet<LaundryOrder> LaundryOrders { get; set; }
            = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LaundryOrder>()
                .Property(order => order.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}

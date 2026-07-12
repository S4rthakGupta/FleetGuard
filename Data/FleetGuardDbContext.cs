using FleetGuard.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetGuard.Data
{
    public class FleetGuardDbContext : DbContext
    {
        public FleetGuardDbContext(
            DbContextOptions<FleetGuardDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; } = null!;

        public DbSet<DiagnosticsLog> DiagnosticsLog
        { get; set; } = null!;

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasIndex(device => device.SerialNumber)
                .IsUnique();
        }
    }
}
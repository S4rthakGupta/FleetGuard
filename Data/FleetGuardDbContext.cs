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
    }
}
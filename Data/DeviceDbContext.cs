using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using VideoConference.Entities;

namespace VideoConference.Data
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options)
        {

        }

        public DbSet<Device> Devices { get; set; }
    }
}

using BPKBAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BPKBAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<BPKB> BPKBs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

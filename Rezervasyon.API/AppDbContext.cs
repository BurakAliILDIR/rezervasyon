using Microsoft.EntityFrameworkCore;
using Rezervasyon.API.Entities;

namespace Rezervasyon.API
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Tren> Trenler { get; set; }
        public virtual DbSet<Vagon> Vagonlar { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
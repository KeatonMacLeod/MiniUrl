using Microsoft.EntityFrameworkCore;
using MiniUrl.Models;

namespace MiniUrl.Database
{
    public class MiniUrlDbContext : DbContext
    {
        public MiniUrlDbContext(DbContextOptions<MiniUrlDbContext> options) : base(options)
        {
        }

        public DbSet<UrlMapping> UrlMappings { get; set; }
    }
}

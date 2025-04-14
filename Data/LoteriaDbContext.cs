using Microsoft.EntityFrameworkCore;
using LoteriaWeb.Models;

namespace LoteriaWeb.Data
{
    public class LoteriaDbContext : DbContext
    {
        public LoteriaDbContext(DbContextOptions<LoteriaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aposta> Apostas { get; set; }
    }
}

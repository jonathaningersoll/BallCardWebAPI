using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

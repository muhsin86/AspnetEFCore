using labb3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labb3.DB
{
    public class GenraContext : DbContext
    {
        public GenraContext(DbContextOptions<GenraContext> options)
        : base(options)
        {
        }
        public DbSet<Genra> Genra { get; set; }
        public DbSet<Artist> Artist { get; set; }
    }
}

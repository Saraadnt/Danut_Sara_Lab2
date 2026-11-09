using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;

namespace Danut_Sara_Lab2.Data
{
    public class Danut_Sara_Lab2Context : DbContext
    {
        public Danut_Sara_Lab2Context(
            DbContextOptions<Danut_Sara_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Danut_Sara_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;

        public DbSet<Authors> Authors { get; set; } = default!;
    }
}

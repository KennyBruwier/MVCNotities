using Microsoft.EntityFrameworkCore;
using MVCNotities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNotities.Database
{
    public class NotitiesContext : DbContext
    {
        public NotitiesContext(DbContextOptions<NotitiesContext> options) : base(options)
        {

        }

        public DbSet<Notitie> Notities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using _ZEEN.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _ZEEN.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<RegularUser> RegularUsers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }




    }
}

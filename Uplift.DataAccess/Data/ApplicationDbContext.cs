using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uplift.Models;

namespace Uplift.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Frequency> frequencies  { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<OrderHeder> orderHeders  { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<ApplicationUser>  applicationUsers { get; set; }
    }
}

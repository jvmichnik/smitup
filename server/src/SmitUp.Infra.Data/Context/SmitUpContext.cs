using Microsoft.EntityFrameworkCore;
using SmitUp.Customers.Infra.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmitUp.Infra.Data.Context
{
    public class SmitUpContext : DbContext
    {
        public SmitUpContext(DbContextOptions<SmitUpContext> options) 
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .SetConfigurationCustomer();

            base.OnModelCreating(modelBuilder);
        }
    }
}

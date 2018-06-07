﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmitUp.Infra.CrossCutting.Identity.Mapping;
using SmitUp.Infra.CrossCutting.Identity.Models;
using System;

namespace SmitUp.Infra.CrossCutting.Identity.Data
{
    public class SmitUpIdentityDbContext : IdentityDbContext<User, Role, Guid>
    {
        public SmitUpIdentityDbContext(DbContextOptions<SmitUpIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMapping());

        }
    }
}

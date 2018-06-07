using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmitUp.Infra.CrossCutting.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmitUp.Infra.CrossCutting.Identity.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable("user", "account");


            b.Property(x => x.Id)
                .HasColumnName("id");

            b.Property(x => x.AccessFailedCount)
                .HasColumnName("access_failed_count");

            b.Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken()
                .HasColumnName("concurrency_stamp");

            b.Property(x => x.Email)
                .HasMaxLength(256)
                .HasColumnName("email");

            b.Property(x => x.EmailConfirmed)
                .HasColumnName("email_confirmed");

            b.Property(x => x.LockoutEnabled)
                .HasColumnName("lockout_enabled");

            b.Property(x => x.LockoutEnd)
                .HasColumnName("lockout_end");

            b.Property(x => x.NormalizedEmail)
                .HasMaxLength(256)
                .HasColumnName("normalized_email");

            b.Property(x => x.NormalizedUserName)
                .HasMaxLength(30)
                .HasColumnName("normalized_username");

            b.Property(x => x.PasswordHash)
                .HasColumnName("password_hash");

            b.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number");

            b.Property(x => x.PhoneNumberConfirmed)
                .HasColumnName("phone_number_confirmed");

            b.Property(x => x.SecurityStamp)
                .HasColumnName("security_stamp");

            b.Property(x => x.TwoFactorEnabled)
                .HasColumnName("two_factor_enabled");

            b.Property(x => x.UserName)
                .HasMaxLength(30)
                .HasColumnName("username");

        }
    }
}

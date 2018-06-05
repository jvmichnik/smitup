using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmitUp.Customers.Domain.Entities;

namespace SmitUp.Customers.Infra.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
;

            builder.ToTable("customer", "account");
                
            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .HasColumnName("name");

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .HasColumnName("email");

            builder.Property(x => x.Gender)
                .HasMaxLength(1)
                .HasColumnName("gender");

            builder.Property(x => x.Birthday)
                .HasColumnName("birthday")
                .HasColumnType("date");

            builder.Property(x => x.MaritalStatus)
                .HasColumnName("marital_status");

            builder.Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(x => x.UserId);
                
        }
    }
}

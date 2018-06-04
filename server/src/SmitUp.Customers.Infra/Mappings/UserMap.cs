using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmitUp.Customers.Domain.Entities;

namespace SmitUp.Customers.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("user", "account");

            builder.Property(x => x.Username)
                .HasMaxLength(30)
                .HasColumnName("username");

            builder.Property(x => x.Password)
                .HasMaxLength(70)
                .HasColumnName("password");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmitUp.Domain.Core.Events;

namespace SmitUp.Infra.Data.Mappings
{
    public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("stored_event", "event");

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.AggregateId)
                .HasColumnName("aggregate_id");

            builder.Property(c => c.Data)
                .HasColumnName("data");

            builder.Property(c => c.MessageType)
                .HasColumnName("action");

            builder.Property(c => c.Timestamp)
                .HasColumnName("creation_date");

            builder.Property(c => c.User)
                .HasColumnName("user");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Shared.Core.Persistence;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("Logs");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.EventCode)
            .HasColumnName("EventCode")
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(x => x.RoomCode)
            .HasColumnName("RoomCode")
            .HasColumnType("TEXT");

        builder.Property(x => x.UserUuid)
            .HasColumnName("UserUuid")
            .HasColumnType("TEXT");

        builder.Property(x => x.Value)
            .HasColumnName("Value")
            .HasColumnType("TEXT");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasIndex(x => x.RoomCode)
            .HasDatabaseName("IX_Logs_RoomCode");
    }
}

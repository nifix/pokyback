using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Shared.Core.Persistence;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> entity)
    {
        entity.ToTable("Rooms");
        entity.HasKey(r => r.Id);

        entity.Property(r => r.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        entity.Property(r => r.Code)
            .HasColumnName("Code")
            .HasColumnType("TEXT");

        entity.Property(r => r.CreatedBy)
            .HasColumnName("CreatedBy")
            .HasColumnType("TEXT");

        entity.Property(r => r.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("datetime");

        entity.Property(r => r.DeletedOn)
            .HasColumnName("DeletedOn")
            .HasColumnType("datetime");

        entity.Property(r => r.CreatedByUuid)
            .HasColumnName("CreatedByUuid")
            .HasColumnType("TEXT");

        entity.Property(r => r.IsRevealed)
            .HasColumnName("IsRevealed")
            .HasColumnType("INTEGER")
            .HasDefaultValue(false);

        entity.Property(r => r.Topic)
            .HasColumnName("Topic")
            .HasColumnType("TEXT")
            .HasDefaultValue("None");

        entity.HasIndex(r => r.Code)
            .HasDatabaseName("rooms_code_index");
        
        entity.HasMany(r => r.RoomUsers)
            .WithOne(ru => ru.Room) // Navigation property in RoomUser
            .HasForeignKey(ru => ru.RoomCode) // Foreign key property in RoomUser
            .HasPrincipalKey(r => r.Code); // Principal key property in Room
        
        entity.HasMany(r => r.Logs)
            .WithOne(l => l.Room)
            .HasForeignKey(l => l.RoomCode)
            .HasPrincipalKey(r => r.Code);
    }
}
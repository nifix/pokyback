using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Shared.Core.Persistence;

public class RoomUserEntityTypeConfiguration : IEntityTypeConfiguration<RoomUser>
{
    public void Configure(EntityTypeBuilder<RoomUser> builder)
    {
        builder.ToTable("RoomUsers");

        builder.HasKey(ru => ru.Id);

        builder.Property(ru => ru.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(ru => ru.Uuid)
            .HasColumnName("Uuid")
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(ru => ru.RoomCode)
            .HasColumnName("RoomCode")
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(ru => ru.JoinedAt)
            .HasColumnName("JoinedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(ru => ru.Username)
            .HasColumnName("Username")
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(ru => ru.CurrentPick)
            .HasColumnName("CurrentPick")
            .HasColumnType("integer")
            .IsRequired(false);

        builder.HasOne(ru => ru.Room)
            .WithMany(r => r.RoomUsers)
            .HasForeignKey(ru => ru.RoomCode)
            .HasPrincipalKey(r => r.Code)
            .HasConstraintName("RoomUsers_RoomsCode_fk");
    }
}
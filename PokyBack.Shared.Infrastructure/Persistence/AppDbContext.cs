using Microsoft.EntityFrameworkCore;
using PokyBack.Shared.Core.Entities;

namespace PokyBack.Shared.Infrastructure.Persistence;

public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options,
    Action<ModelBuilder> configureAction)
    : DbContext(options)
{
    /// <summary>
    /// Represents the collection of Room entities in the database.
    /// </summary>
    /// <remarks>
    /// This property is part of the AppDbContext and is used to interact with the Rooms
    /// table in the database. It allows performing CRUD operations on Room entities.
    /// </remarks>
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomUser> RoomUsers { get; set; }
    public DbSet<Log> Logs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder? modelBuilder)
    {
        if (modelBuilder is null) 
            return;
        
        base.OnModelCreating(modelBuilder);
        configureAction.Invoke(modelBuilder);
    }
}
using HomeAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAPI.Data;

public sealed class HomeAPIContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Device> Devices { get; set; }

    public HomeAPIContext(DbContextOptions<HomeAPIContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().ToTable("Rooms");
        modelBuilder.Entity<Device>().ToTable("Devices");
    }
}
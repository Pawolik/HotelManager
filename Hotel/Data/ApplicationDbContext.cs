namespace  HotelManager.Data;
using HotelManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;


public class ApplicationDbContext : DbContext
{

    public DbSet<Client> Clients { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacja Client -> Reservation
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacja Room -> Reservation
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Room)
            .WithMany(ro => ro.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }



}

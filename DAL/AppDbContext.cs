using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<BookingRoom> BookingRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
           .HasMany(b => b.BookingRooms)
           .WithOne(br => br.Booking)
           .HasForeignKey(br => br.BookingId);

            modelBuilder.Entity<BookingRoom>()
                .HasOne(br => br.Room)
                .WithMany(r => r.BookingRooms)
                .HasForeignKey(br => br.RoomId);

            modelBuilder.Entity<BookingRoom>()
        .HasOne(br => br.Booking)
        .WithMany(b => b.BookingRooms)
        .HasForeignKey(br => br.BookingId)
        .OnDelete(DeleteBehavior.Restrict); // or .OnDelete(DeleteBehavior.NoAction)

            modelBuilder.Entity<BookingRoom>()
                .HasOne(br => br.Room)
                .WithMany(r => r.BookingRooms)
                .HasForeignKey(br => br.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // or .OnDelete(DeleteBehavior.NoAction)




            base.OnModelCreating(modelBuilder);
        }


    }
}

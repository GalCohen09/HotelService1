using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelService1.Models
{
    public class PayoneerMailingContext : DbContext
    {
        public PayoneerMailingContext()
        {
        }

        public PayoneerMailingContext(DbContextOptions<PayoneerMailingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BedType> BedTypes { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationRoom> ReservationRooms { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=sql.dev.payoneer.com;Database=PayoneerMailing;User Id=galco;Password=dev01@@Erezegry1234!");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BedType>(entity =>
            {
                entity.ToTable("BedType", "Training");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation", "Training");
                entity.HasKey(x => x.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
                //entity.Navigation(x => x.ReservationsRooms);
            });

            modelBuilder.Entity<ReservationRoom>(entity =>
            {
                entity.HasKey(x=>new {x.RoomNumber, x.ReservationId});

                entity.ToTable("ReservationRoom", "Training");

               // entity.Property(e => e.Id).HasColumnName("id");

                //entity.HasOne<Reservation>(d => d.Reservation)
                //    .WithMany(d => d.ReservationsRooms)
                //    .HasForeignKey(d => d.ReservationId);
                //.HasConstraintName("FK__Reservati__Reser__122052C0");

                //entity.HasOne<Room>(d => d.Room)
                //    .WithMany(d => d.ReservationsRooms)
                //    .HasForeignKey(d => d.RoomNumber);
                   // .HasConstraintName("FK__Reservati__RoomN__131476F9");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomNumber);
                    //.HasName("PK__Room__AE10E07BC545D090");

                entity.ToTable("Room", "Training");

                entity.Property(e => e.RoomNumber);//.ValueGeneratedNever();

                //entity.Property(e => e.ChosenBedType)
                //    .HasMaxLength(50)
                //    .IsUnicode(false);

                //entity.HasOne(d => d.BedTypeNavigation)
                //    .WithMany(p => p.Rooms)
                //    .HasForeignKey(d => d.BedType)
                //    .HasConstraintName("FK__Room__BedType__7F0D7E4C");
                //entity.Navigation(x => x.ReservationsRooms);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

       //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

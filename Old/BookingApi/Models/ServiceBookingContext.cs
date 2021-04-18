using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookingApi.Models
{
    public partial class ServiceBookingContext : DbContext
    {
        public ServiceBookingContext()
        {
        }

        public ServiceBookingContext(DbContextOptions<ServiceBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KANINI-LTP-329;Database=ServiceBooking;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");


                entity.Property(e => e.Bookingstatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Endtime).HasColumnName("endtime");

                entity.Property(e => e.Estimatedcost).HasColumnName("estimatedcost");

                entity.Property(e => e.ServiceProviderId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ServiceProviderID");

                entity.Property(e => e.Servicedate)
                    .HasColumnType("date")
                    .HasColumnName("servicedate");

                entity.Property(e => e.Servicestatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Starttime).HasColumnName("starttime");

                
            });

          

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

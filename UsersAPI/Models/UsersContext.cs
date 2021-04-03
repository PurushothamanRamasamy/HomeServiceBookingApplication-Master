using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UsersAPI.Models
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserServiceInfo> UserServiceInfos { get; set; }

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

            /*modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Bookingid).ValueGeneratedNever();

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

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Booking__Custome__534D60F1");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.BookingServiceProviders)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .HasConstraintName("FK__Booking__Service__5441852A");
            });

            modelBuilder.Entity<SpecializationTable>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Speciali__737584F7320928DB");

                entity.ToTable("SpecializationTable");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecificationTable>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Specific__737584F756FAF5DF");

                entity.ToTable("SpecificationTable");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecializationName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SpecializationNameNavigation)
                    .WithMany(p => p.SpecificationTables)
                    .HasForeignKey(d => d.SpecializationName)
                    .HasConstraintName("FK__Specifica__Speci__4BAC3F29");
            });*/

            modelBuilder.Entity<UserServiceInfo>(entity =>
            {
                entity.HasKey(e => e.Usid)
                    .HasName("PK__UserServ__AA7CE51D76D27BF3");

                entity.ToTable("UserServiceInfo");

                entity.HasIndex(e => e.Username, "UQ__UserServ__536C85E471F753EB")
                    .IsUnique();

                entity.Property(e => e.Usid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USid");

                entity.Property(e => e.Aadhaarno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Costperhour).HasColumnName("costperhour");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phoneno)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.ServiceCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Specialization)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("specialization");

                entity.Property(e => e.Specification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

               
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SpecilizationAPI.Models
{
    public partial class SpecializationContext : DbContext
    {
       

        public SpecializationContext(DbContextOptions<SpecializationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SpecializationTable> Specializations { get; set; }

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


            modelBuilder.Entity<SpecializationTable>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Speciali__737584F7320928DB");

                entity.ToTable("SpecializationTable");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

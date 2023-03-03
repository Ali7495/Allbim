using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Logging.LogModels
{
    public partial class AlbimLogDbContext : DbContext
    {

        public AlbimLogDbContext(DbContextOptions<AlbimLogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HandledErrorLog> HandledErrorLogs { get; set; }
        public virtual DbSet<OperationLog> OperationLogs { get; set; }
        public virtual DbSet<SystemErrorLog> SystemErrorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<HandledErrorLog>(entity =>
            {
                entity.ToTable("HandledErrorLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErrorCode).HasMaxLength(100);

                entity.Property(e => e.ErrorMessage).HasMaxLength(300);

                entity.Property(e => e.MethodName).HasMaxLength(300);

                entity.Property(e => e.ServiceName).HasMaxLength(300);
            });

            modelBuilder.Entity<OperationLog>(entity =>
            {
                entity.ToTable("OperationLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthenticatedUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ExecuteTime).HasMaxLength(50);

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StatusCode).HasMaxLength(50);
            });

            modelBuilder.Entity<SystemErrorLog>(entity =>
            {
                entity.ToTable("SystemErrorLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceName).HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

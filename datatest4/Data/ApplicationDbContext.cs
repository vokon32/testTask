using datatest4.Models;
using Microsoft.EntityFrameworkCore;

namespace datatest4.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TrackLocation> TrackLocations { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Prog;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackLocation>(entity =>
            {
                entity.ToTable("TrackLocation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateEvent).HasColumnType("datetime");

                entity.Property(e => e.DateTrack)
                    .HasColumnType("datetime")
                    .HasColumnName("date_track");

                entity.Property(e => e.Imei)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IMEI");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(12, 9)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(12, 9)")
                    .HasColumnName("longitude");

                entity.Property(e => e.TypeSource).HasDefaultValueSql("((1))");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

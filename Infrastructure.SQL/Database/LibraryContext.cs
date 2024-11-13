using Infrastructure.SQL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQL.Database
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<VideoGameEntity> VideoGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<VideoGameEntity>();
            builder.ToTable("VideoGames", "dbo");
            builder.HasIndex(p => p.Name).IsUnique(true);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Studio).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Genres).HasMaxLength(200).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}

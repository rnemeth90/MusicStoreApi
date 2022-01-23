using Microsoft.EntityFrameworkCore;
using MusicApi.Models;

namespace MusicApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
                    new Song
                    {
                        Id = 1,
                        Title = "Willow",
                        Language = "en",
                        Duration = "4:35"
                    },
                    new Song
                    {
                        Id = 2,
                        Title = "Another Song",
                        Language = "en",
                        Duration = "4:35"
                    }
                );
        }
    }
}

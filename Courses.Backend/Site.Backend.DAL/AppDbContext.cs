using Microsoft.EntityFrameworkCore;
using Site.Domain.Course.Entity;
using Site.Domain.User.Entity;

namespace Site.Backend.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseEntity>()
                .HasMany(t => t.Students)
                .WithMany(t => t.Courses)
                .UsingEntity(j => j.ToTable("CoursesUsers"));
        }
    }

}

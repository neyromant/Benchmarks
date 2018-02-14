
using AsNoTracking.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsNoTracking
{
    public class TestDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(grant =>
            {
                grant.Property(x => x.Id).ValueGeneratedNever();
                grant.Property(x => x.Name).HasMaxLength(50).IsRequired();
                grant.HasKey(x => x.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

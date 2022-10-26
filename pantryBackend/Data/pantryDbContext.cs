using Microsoft.EntityFrameworkCore;
using pantryBackend.Models;

namespace pantryBackend.Data
{
    public class pantryDbContext:DbContext
    {
        public pantryDbContext(DbContextOptions<pantryDbContext> options) : base(options)
        {

        }
        public DbSet<pantryItems> pantryItems { get; set; }
        public DbSet<recipes> recipe { get; set; }
        public DbSet<pantryItemsrecipes> pantryItemsrecipes { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<steps> steps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<pantryItems>().ToTable("pantryItems");
            modelBuilder.Entity<recipes>().ToTable("recipe");
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<steps>().ToTable("steps");
            modelBuilder.Entity<pantryItemsrecipes>().ToTable("pantryItemsrecipes");
            modelBuilder.Entity<stepsrecipes>().ToTable("stepsrecipes");
            modelBuilder.Entity<recipes>().HasMany(r => r.pantryItem);
            modelBuilder.Entity<recipes>().HasMany(r => r.step);
            modelBuilder.Entity<pantryItems>().HasMany(r => r.recipe);
            modelBuilder.Entity<pantryItemsrecipes>().HasKey(p => new { p.pantryItempid, p.reciperid });
            modelBuilder.Entity<stepsrecipes>().HasKey(p => new { p.stepid, p.reciperid });









        }
    }
}

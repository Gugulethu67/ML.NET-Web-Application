using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Team21V4._5.Model;
using Team21V4_5;

namespace Team21V4._5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team21V4._5.Model.Product>? Products { get; set; }
        public DbSet<Team21V4._5.Model.Prediction>? Predictions { get; set; }
        public DbSet<MLModel.ModelOutput> Recommendations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Prediction>().ToTable("Prediction");
            modelBuilder.Entity<MLModel.ModelOutput>().ToTable("Recommendations");

        }

    }
}
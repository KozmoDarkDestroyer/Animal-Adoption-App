using animal_adoption.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace animal_adoption.context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Adopter>()
                .HasIndex(k => k.identification)
                .IsUnique();
            
            modelBuilder.Entity<Adopter>()
                .HasIndex(k => k.email)
                .IsUnique();
                
            modelBuilder.Entity<Foundation>()
                .HasIndex(k => k.email)
                .IsUnique();    
        }

        public DbSet<Pet> Pet { get; set; }
        public DbSet<Foundation> Foundation { get; set; }
        public DbSet<Adopter> Adopter { get; set; }
        public DbSet<Form> Form { get; set; }

    }
}
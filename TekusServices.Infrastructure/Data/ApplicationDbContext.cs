using Microsoft.EntityFrameworkCore;
using TekusServices.Domain.Entities;

namespace TekusServices.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ProviderCustomField> ProviderCustomFields { get; set; }
        public DbSet<Service> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Provider -> Services (uno a muchos)
            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Services)
                .WithOne(s => s.Provider)
                .HasForeignKey(s => s.ProviderId);

            // Relación Provider -> CustomFields (uno a muchos)
            modelBuilder.Entity<Provider>()
                .HasMany(p => p.CustomFields)
                .WithOne(cf => cf.Provider)
                .HasForeignKey(cf => cf.ProviderId);

            // Nit unico
            modelBuilder.Entity<Provider>()
                .HasIndex(p => p.Nit)
                .IsUnique();
        }
    }
}

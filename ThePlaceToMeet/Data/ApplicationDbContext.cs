using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThePlaceToMeet.Data.Mappers;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public DbSet<Catering> Caterings { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Vergaderruimte> Vergaderruimtes { get; set; }
        public DbSet<Korting> Kortingen { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CateringConfiguration());
            builder.ApplyConfiguration(new KlantConfiguration());
            builder.ApplyConfiguration(new KortingConfiguration());
            builder.ApplyConfiguration(new ReservatieConfiguration());
            builder.ApplyConfiguration(new VergaderruimteConfiguration());
        }
    }
}

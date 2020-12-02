using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Mappers
{
    public class KlantConfiguration : IEntityTypeConfiguration<Klant>
    {
        public void Configure(EntityTypeBuilder<Klant> builder)
        {
            builder.ToTable("Klant");
            builder.HasKey(t => t.KlantCode);
            builder.Property(t => t.Naam).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Voornaam).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(100);

            builder.HasMany(t => t.Reservaties).WithOne().IsRequired(true).OnDelete(DeleteBehavior.Restrict);
        }
    }
}


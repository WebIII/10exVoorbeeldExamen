using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Mappers
{
    public class CateringConfiguration : IEntityTypeConfiguration<Catering>
    {
        public void Configure(EntityTypeBuilder<Catering> builder)
        {
            builder.ToTable("Catering");
            builder.Property(t => t.Titel).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Beschrijving).IsRequired();
        }
    }
}


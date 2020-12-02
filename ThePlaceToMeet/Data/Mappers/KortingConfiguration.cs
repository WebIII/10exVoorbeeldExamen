using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Mappers
{
    public class KortingConfiguration : IEntityTypeConfiguration<Korting>
    {
        public void Configure(EntityTypeBuilder<Korting> builder)
        {
            builder.ToTable("Korting");
        }
    }
}


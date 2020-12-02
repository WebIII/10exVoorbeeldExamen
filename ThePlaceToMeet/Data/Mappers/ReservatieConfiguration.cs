using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Mappers
{
    public class ReservatieConfiguration : IEntityTypeConfiguration<Reservatie>
    {
        public void Configure(EntityTypeBuilder<Reservatie> builder)
        {
            //implemsenteer
        }
    }
}


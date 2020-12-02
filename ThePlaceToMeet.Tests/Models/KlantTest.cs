using ThePlaceToMeet.Models.Domain;
using ThePlaceToMeet.Tests.Data;
using Xunit;

namespace ThePlaceToMeet.Tests.Models
{
    public class KlantTest
    {
        private DummyDbContext _context;
        private Klant klantMetTweeReservaties;
        private Klant klantZonderReservaties;
        private int jaarVanReservaties;

        public KlantTest()
        {
            _context = new DummyDbContext();
            klantMetTweeReservaties = _context.Peter;
            klantZonderReservaties = _context.Piet;
            jaarVanReservaties = _context.Dag.Year;
        }

        [Fact]
        public void GetAantalReservaties_KlantMetReservatiesVoorGegevenJaar_RetourneertAantalReservatiesInJaar()
        {
            Assert.Equal(2, klantMetTweeReservaties.GetAantalReservaties(jaarVanReservaties));
        }

        [Fact]
        public void GetAantalReservaties_KlantZonderReservatiesVoorGegevenJaar_RetourneertNul()
        {
            Assert.Equal(0, klantMetTweeReservaties.GetAantalReservaties(jaarVanReservaties + 10));
        }

        [Fact]
        public void GetAantalReservaties_KlantZonderReservaties_RetourneertNul()
        {
            Assert.Equal(0, klantZonderReservaties.GetAantalReservaties(jaarVanReservaties));
        }
    }
}

using System;
using System.Linq;
using ThePlaceToMeet.Models.Domain;
using ThePlaceToMeet.Tests.Data;
using Xunit;

namespace ThePlaceToMeet.Tests.Models
{
    public class VergaderruimteTest
    {
        private readonly DummyDbContext _context;
        private readonly Vergaderruimte _vergaderruimte;
        private readonly Klant _klantMetVierReservaties;
        private readonly Klant _klantMetTweeReservaties;
        private readonly Klant _klantZonderReservaties;

        public VergaderruimteTest()
        {
            _context = new DummyDbContext();
            _vergaderruimte = _context.Vergaderruimte;
            _klantMetVierReservaties = _context.Jan;
            _klantMetTweeReservaties = _context.Peter;
            _klantZonderReservaties = _context.Piet;
        }

        [Fact(Skip = "Not yet implemented")]
        public void Reserveer_DatumLigtNietInDeToekomst_WerptArgumentException()
        {
        }

        [Theory]
        [InlineData(5, 2, 10)] // begint te vroeg
        [InlineData(23, 2, 10)] // begint te laat
        [InlineData(20, 5, 10)] // eindigt te laat
        [InlineData(20, 1, 10)] // duur is te kort
        [InlineData(8, 2, 90)] // teveel personen
        public void Reserveer_OngeldigeGegevens_WerptArgumentException(int beginUur, int aantalUren, int aantalPersonen)
        {
            Assert.Throws<ArgumentException>(() => _vergaderruimte.Reserveer(_klantZonderReservaties, _context.Kortingen, _context.Dag.AddDays(20), beginUur, aantalUren, aantalPersonen, null, true));
        }

        [Theory]
        [InlineData(8, 2)]
        [InlineData(10, 3)]
        [InlineData(12, 2)]
        [InlineData(8, 5)]
        public void Reserveer_VergaderruimteIsReedsGereserveerd_WerptArgumentException(int beginUur, int aantalUren)
        {
            Assert.Throws<ArgumentException>(() => _vergaderruimte.Reserveer(_klantMetTweeReservaties, _context.Kortingen, _context.Dag.AddDays(8), beginUur, aantalUren, 10, null, true));
        }

        [Fact]
        public void Reserveer_DatumTeVroegVoorCatering_WerptArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _vergaderruimte.Reserveer(_klantZonderReservaties, _context.Kortingen, DateTime.Today.AddDays(3), 20, 2, 10, _context.CateringSushi, true));
        }

        [Theory]
        [InlineData(10, 3)] // normaal geval
        [InlineData(8, 3)]  // grenswaarde beginuur
        [InlineData(19, 3)] // grenswaarde einduur
        [InlineData(8, 14)] // grenswaarden begin- en einduur
        [InlineData(19, 2)] // grenswaarde duur
        public void Reserveer_GeldigeGegevens_CreeertReservatie(int beginUur, int aantalUren)
        {
            Reservatie r = _vergaderruimte.Reserveer(_klantZonderReservaties, _context.Kortingen, _context.Dag.AddDays(12), beginUur, aantalUren, 10, _context.CateringSushi, true);
            Assert.Equal(7, _vergaderruimte.Reservaties.Count);
            Assert.Equal(1, _klantZonderReservaties.Reservaties.Count);
            Assert.Single(_vergaderruimte.Reservaties, re => re == r); // de reservatie zit in de lijst van reservaties van de vergaderruimte
            Assert.Single(_klantZonderReservaties.Reservaties, re => re == r); // de reservatie zit in de lijst van reservaties van de klant
        }

        [Fact]
        public void Reserveer_ZonderRechtOpKorting_CreeertReservatieZonderKorting()
        {
            Reservatie r = _vergaderruimte.Reserveer(_klantZonderReservaties, _context.Kortingen, DateTime.Today.AddDays(12), 20, 2, 10, _context.CateringSushi, true);
            Assert.Null(_klantZonderReservaties.Reservaties.Last().Korting);
        }

        [Fact]
        public void Reserveer_MetRechtOpKleineKorting_CreeertReservatieMetKorting()
        {
            Reservatie r1 = _vergaderruimte.Reserveer(_klantMetTweeReservaties, _context.Kortingen, _context.Dag.AddDays(20), 8, 3, 10, _context.CateringSushi, true);
            Assert.Equal(_context.Kortingen.First(), r1.Korting);
            Reservatie r2 = _vergaderruimte.Reserveer(_klantMetTweeReservaties, _context.Kortingen, _context.Dag.AddDays(21), 8, 3, 10, _context.CateringSushi, true);
            Assert.Equal(_context.Kortingen.First(), r2.Korting);
        }

        [Fact]
        public void Reserveer_MetRechtOpGroteKorting_CreeertReservatieMetKorting()
        {
            Reservatie r1 = _vergaderruimte.Reserveer(_klantMetVierReservaties, _context.Kortingen, _context.Dag.AddDays(20), 8, 3, 10, _context.CateringSushi, true);
            Assert.Equal(_context.Kortingen.Last(), r1.Korting);
            Reservatie r2 = _vergaderruimte.Reserveer(_klantMetVierReservaties, _context.Kortingen, _context.Dag.AddDays(21), 8, 3, 10, _context.CateringSushi, true);
            Assert.Equal(_context.Kortingen.Last(), r2.Korting);
        }
    }
}

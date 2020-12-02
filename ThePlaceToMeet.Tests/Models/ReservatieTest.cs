using System;
using System.Linq;
using ThePlaceToMeet.Models.Domain;
using ThePlaceToMeet.Tests.Data;
using Xunit;

namespace ThePlaceToMeet.Tests.Models
{
    public class ReservatieTest
    {
        private readonly DummyDbContext _context;
        private readonly Vergaderruimte _vergaderruimte;

        public ReservatieTest()
        {
            _context = new DummyDbContext();
            _vergaderruimte = _context.Vergaderruimte;
        }

        [Fact]
        public void TotalePrijs_ReservatieZonderCateringEnZonderKorting_RetourneertTotalePrijs()
        {
            Reservatie r = new Reservatie() { Dag = _context.Dag.AddDays(12), AantalPersonen = 10, BeginUur = 8, PrijsPerUur = 15, DuurInUren=3 };
            Assert.Equal(45, r.TotalePrijs);
        }

        [Fact]
        public void TotalePrijs_ReservatieMetCateringEnZonderKorting_RetourneertTotalePrijs()
        {
            Reservatie r = new Reservatie() { Dag = _context.Dag.AddDays(12), AantalPersonen = 10, BeginUur = 8, PrijsPerUur = 15, DuurInUren = 3, Catering=_context.CateringSushi, PrijsPerPersoonCatering=11, PrijsPerPersoonStandaardCatering=9};
            Assert.Equal(245M, r.TotalePrijs);
        }

        [Fact]
        public void TotalePrijs_ReservatieMetCateringEnMetKorting_RetourneertTotalePrijs()
        {
            Reservatie r = new Reservatie() { Dag = _context.Dag.AddDays(12), AantalPersonen = 10, BeginUur = 8, PrijsPerUur = 15, DuurInUren = 3, Catering = _context.CateringSushi, PrijsPerPersoonCatering = 11, PrijsPerPersoonStandaardCatering = 9, Korting = _context.Kortingen.First() };
            Assert.Equal(242.75M, r.TotalePrijs);
        }

        [Fact]
        public void TotalePrijs_ReservatieZonderCateringEnMetKorting_RetourneertTotalePrijs()
        {
            Reservatie r = new Reservatie() { Dag = DateTime.Today.AddDays(12), AantalPersonen = 10, BeginUur = 8, PrijsPerUur = 15, DuurInUren = 3, Korting = _context.Kortingen.First() };
            Assert.Equal(42.75M, r.TotalePrijs);
        }
    }
}


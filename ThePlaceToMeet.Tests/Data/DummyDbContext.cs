using System;
using System.Collections.Generic;
using System.Linq;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Tests.Data
{
    public class DummyDbContext
    {
        public ICollection<Vergaderruimte> Vergaderruimtes { get; }
        public IEnumerable<Vergaderruimte> Vergaderruimtes20 => Vergaderruimtes.Where(v => v.MaximumAantalPersonen >= 20);
        public IEnumerable<Catering> Caterings { get; }
        public IEnumerable<Korting> Kortingen { get; }
        public Klant Peter { get; } // een klant met 2 reservaties
        public Klant Jan { get; } // een klant met 4 reservaties
        public Klant Piet { get; } // een klant zonder reservaties
        public Vergaderruimte Vergaderruimte { get; } // een vergaderruimte met 6 reservaties
        public Catering CateringSushi { get; }
        public DateTime Dag { get; } // 01/08 van volgend jaar

        public DummyDbContext()
        {
            Vergaderruimtes = new List<Vergaderruimte>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    Vergaderruimte r = new Vergaderruimte() { Id = (i * 3) + j, Naam = $"{((VergaderruimteType)i).ToString() } {j * 10} personen", MaximumAantalPersonen = j * 10, PrijsPerPersoonStandaardCatering = 10, PrijsPerUur = (15 + i) * j, VergaderruimteType = (VergaderruimteType)i };
                    Vergaderruimtes.Add(r);
                }
            }

            Catering cateringSalad = new Catering() { Id = 1, Titel = "Salad in a jar", Beschrijving = "Salad in a jar", PrijsPerPersoon = 11 };
            Catering cateringBroodjes = new Catering() { Id = 2, Titel = "Broodjes", Beschrijving = "Broodjes", PrijsPerPersoon = 8 };
            CateringSushi = new Catering() { Id = 3, Titel = "Sushi - Sashimi", Beschrijving = "Sushi - Sashimi", PrijsPerPersoon = 12 };
            Caterings = new List<Catering> { cateringSalad, cateringBroodjes, CateringSushi };

            Korting korting1 = new Korting() { MinimumAantalReservatiesInJaar = 3, Percentage = 5 };
            Korting korting2 = new Korting() { MinimumAantalReservatiesInJaar = 5, Percentage = 10 };
            Kortingen = new List<Korting> { korting1, korting2 };

            Peter = new Klant() { Naam = "Claeyssens", Voornaam = "Peter", Email = "peter@hogent.be", Bedrijf = "HoGent" };
            Jan = new Klant() { Naam = "Peeters", Voornaam = "Jan", Email = "jan@gmail.com", Bedrijf = "HoGent" };
            Piet = new Klant() { Naam = "Pieters", Voornaam = "Piet", Email = "piet@gmail.com", Bedrijf = "HoGent" };

            Dag = new DateTime(DateTime.Now.Year + 1, 8, 1);
            Vergaderruimte = Vergaderruimtes.First();
            Reservatie res = new Reservatie() { Dag = Dag.AddDays(8), BeginUur = 8, DuurInUren = 5, AantalPersonen = 10, Catering = cateringBroodjes, PrijsPerPersoonCatering = 10, PrijsPerUur = 10 };
            Peter.VoegReservatieToe(res);
            Vergaderruimte.Reservaties.Add(res);
            res = new Reservatie() { Dag = Dag, BeginUur = 14, DuurInUren = 4, AantalPersonen = 10, PrijsPerPersoonCatering = 10, PrijsPerUur = 10 };
            Peter.VoegReservatieToe(res);
            Vergaderruimte.Reservaties.Add(res);
            res = new Reservatie() { Dag = Dag, BeginUur = 9, DuurInUren = 3, AantalPersonen = 10, PrijsPerPersoonCatering = 12, PrijsPerUur = 10 };
            Jan.VoegReservatieToe(res);
            Vergaderruimte.Reservaties.Add(res);
            res = new Reservatie() { Dag = Dag.AddDays(1), BeginUur = 9, DuurInUren = 3, AantalPersonen = 10, PrijsPerPersoonCatering = 12, PrijsPerUur = 10 };
            Jan.VoegReservatieToe(res);
            Vergaderruimte.Reservaties.Add(res);
            res = new Reservatie() { Dag = Dag.AddDays(2), BeginUur = 9, DuurInUren = 3, AantalPersonen = 10, PrijsPerPersoonCatering = 12, PrijsPerUur = 10 };
            Jan.VoegReservatieToe(res);
            Vergaderruimte.Reservaties.Add(res);
            res = new Reservatie() { Dag = Dag.AddDays(3), BeginUur = 9, DuurInUren = 3, AantalPersonen = 10, PrijsPerPersoonCatering = 12, PrijsPerUur = 10 };
            Jan.VoegReservatieToe(res);
            Vergaderruimte.Reservaties.Add(res);
        }
    }
}

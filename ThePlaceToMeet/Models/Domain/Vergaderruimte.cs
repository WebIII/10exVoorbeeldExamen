using System;
using System.Collections.Generic;
using System.Linq;

namespace ThePlaceToMeet.Models.Domain
{
    public class Vergaderruimte
    {
        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        public VergaderruimteType VergaderruimteType { get; set; }
        public int MaximumAantalPersonen { get; set; }
        public decimal PrijsPerUur { get; set; } //prijs voor huur vergaderruimte per uur
        public decimal PrijsPerPersoonStandaardCatering { get; set; } //prijs voor de standaardcatering (koffie, thee, water) per persoon
        public ICollection<Reservatie> Reservaties { get; private set; }
        #endregion

        #region Constructors and methods
        public Vergaderruimte()
        {
            Reservaties = new List<Reservatie>();
        }

        private IEnumerable<Reservatie> GetReservatiesVoorDag(DateTime dag)
        {
            //implementeer
            return null;
        }

        public Reservatie Reserveer(Klant klant, IEnumerable<Korting> kortingen, DateTime dag, int beginUur, int aantalUren, int aantalPersonen, Catering catering, bool standaardCatering)
        {
            //implementeer
            return null;

        }

        private Korting GetKorting(IEnumerable<Korting> kortingen, int aantalReservaties)
        {
            //implementeer
            return null;
        }
        #endregion
    }
}

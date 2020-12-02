using System;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Controllers
{
    public class ReservatieViewModel
    {
        public string Naam { get; set; }

        public DateTime Dag { get; set; }

        public int BeginUur { get; set; }

        public int Duur { get; set; }

        public int AantalPersonen { get; set; }

        public bool StandaardCatering { get; set; }

        public int CateringId { get; set; }

        public ReservatieViewModel()
        {
            Dag = DateTime.Today.Date.AddDays(7);
            BeginUur = 8;
            Duur = 2;
            AantalPersonen = 1;
        }

        public ReservatieViewModel(Vergaderruimte ruimte) : this()
        {
            Naam = ruimte.Naam;
        }
    }
}
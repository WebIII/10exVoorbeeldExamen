using System.Collections.Generic;
using System.Linq;

namespace ThePlaceToMeet.Models.Domain
{
    public class Klant
    {
        #region Properties
        public int KlantCode { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string GSM { get; set; }
        public string Bedrijf { get; set; }
        public ICollection<Reservatie> Reservaties { get; protected set; }
        #endregion

        #region Constructors and methods
        public Klant()
        {
            Reservaties = new List<Reservatie>();
        }

        public int GetAantalReservaties(int jaar)
        {
            //implementeer
            return 0;
        }

        public void VoegReservatieToe(Reservatie reservatie)
        {
            Reservaties.Add(reservatie);
        }
        #endregion
    }
}

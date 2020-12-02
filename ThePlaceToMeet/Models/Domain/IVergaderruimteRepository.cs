using System.Collections.Generic;

namespace ThePlaceToMeet.Models.Domain
{
    public interface IVergaderruimteRepository
    {
        IEnumerable<Vergaderruimte> GetAll();
        IEnumerable<Vergaderruimte> GetByMaxAantalPersonen(int maxAantalPersonen);
        Vergaderruimte GetById(int id);
        void SaveChanges();
    }
}

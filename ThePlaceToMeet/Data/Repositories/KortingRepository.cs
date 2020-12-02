using System.Collections.Generic;
using System.Linq;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Repositories
{
    public class KortingRepository : IKortingRepository
    {
        private readonly ApplicationDbContext _context;

        public KortingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Korting> GetAll()
        {
            return _context.Kortingen.OrderBy(t=>t.MinimumAantalReservatiesInJaar).ToList();
        }
    }
}

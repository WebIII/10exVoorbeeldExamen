using System.Collections.Generic;
using System.Linq;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Repositories
{
    public class CateringRepository : ICateringRepository
    {
        private readonly ApplicationDbContext _context;

        public CateringRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Catering> GetAll()
        {
            return _context.Caterings.OrderBy(t=>t.Titel).ToList();
        }

        public Catering GetBy(int id)
        {
            return _context.Caterings.SingleOrDefault(t => t.Id == id);
        }
    }
}

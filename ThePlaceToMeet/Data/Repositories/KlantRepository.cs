using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Repositories
{
    public class KlantRepository:IKlantRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Klant> _klanten;

        public KlantRepository(ApplicationDbContext context)
        {
            _context = context;
            _klanten = _context.Klanten;
        }

        public void Add(Klant klant)
        {
            _klanten.Add(klant);
        }

        public Klant GetByEmail(string email)
        {
            return _klanten.Include(t=>t.Reservaties).FirstOrDefault(t => t.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

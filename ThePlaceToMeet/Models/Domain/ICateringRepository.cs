using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePlaceToMeet.Models.Domain
{
    public interface ICateringRepository
    {
        IEnumerable<Catering> GetAll();
        Catering GetBy(int id);
    }
}

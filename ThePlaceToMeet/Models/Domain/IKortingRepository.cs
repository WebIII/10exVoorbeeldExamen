using System.Collections.Generic;

namespace ThePlaceToMeet.Models.Domain
{
    public interface IKortingRepository
    {
        IEnumerable<Korting> GetAll();
    }
}

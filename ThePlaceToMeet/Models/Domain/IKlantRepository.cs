namespace ThePlaceToMeet.Models.Domain
{
    public interface IKlantRepository
    {
        Klant GetByEmail(string email);
        void Add(Klant klant);
        void SaveChanges();
    }
}

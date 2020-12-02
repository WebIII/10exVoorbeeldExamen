using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ThePlaceToMeet.Models.Domain;
using System.Linq;

namespace ThePlaceToMeet.Data
{
    public class ApplicationDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationDataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await InitializeUsers();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        Vergaderruimte r = new Vergaderruimte() { Naam = $"{((VergaderruimteType)i).ToString()} Room - {j} ", MaximumAantalPersonen = j * 10, PrijsPerPersoonStandaardCatering = 10, PrijsPerUur = (15 + i) * j, VergaderruimteType = (VergaderruimteType)i };
                        _dbContext.Vergaderruimtes.Add(r);
                    }
                }
                _dbContext.SaveChanges();

                Catering cateringSalad = new Catering() { Titel = "Salad in a jar", Beschrijving = "Salad in a jar", PrijsPerPersoon = 10 };
                Catering cateringBroodjes = new Catering() { Titel = "Broodjes", Beschrijving = "Broodjes", PrijsPerPersoon = 8 };
                Catering cateringSushi = new Catering() { Titel = "Sushi - Sashimi", Beschrijving = "Sushi - Sashimi", PrijsPerPersoon = 12 };
                Catering[] caterings =
                new Catering[] { cateringSalad, cateringBroodjes, cateringSushi };
                _dbContext.Caterings.AddRange(caterings);
                _dbContext.SaveChanges();

                Korting korting1 = new Korting() { MinimumAantalReservatiesInJaar = 3, Percentage = 5 };
                Korting korting2 = new Korting() { MinimumAantalReservatiesInJaar = 10, Percentage = 10 };
                _dbContext.Kortingen.AddRange(new Korting[] { korting1, korting2 });
                _dbContext.SaveChanges();

                Klant peter = new Klant() { Naam = "Claeyssens", Voornaam = "Peter", Email = "peter@hogent.be", Bedrijf = "HoGent" };
                _dbContext.Klanten.Add(peter);
                Klant jan = new Klant() { Naam = "Peeters", Voornaam = "Jan", Email = "jan@gmail.com", Bedrijf = "HoGent" };
                _dbContext.Klanten.Add(jan);
                _dbContext.SaveChanges();

                Vergaderruimte ruimte = _dbContext.Vergaderruimtes.SingleOrDefault(t => t.Id == 1);
                Reservatie res = new Reservatie() { Dag = DateTime.Today.AddDays(10), BeginUur = 8, DuurInUren = 5, AantalPersonen = 10, Catering = cateringBroodjes, PrijsPerPersoonCatering = 10, PrijsPerUur = 10 };
                peter.VoegReservatieToe(res);
                ruimte.Reservaties.Add(res);
                res = new Reservatie() { Dag = DateTime.Today.AddDays(10), BeginUur = 14, DuurInUren = 4, AantalPersonen = 10, PrijsPerPersoonCatering = 10, PrijsPerUur = 10 };
                peter.VoegReservatieToe(res);
                ruimte.Reservaties.Add(res);
                res = new Reservatie() { Dag = DateTime.Today.AddDays(8), BeginUur = 9, DuurInUren = 4, AantalPersonen = 10, PrijsPerPersoonCatering = 12, PrijsPerUur = 10 };
                jan.VoegReservatieToe(res);
                ruimte.Reservaties.Add(res);
                _dbContext.SaveChanges();
            }
        }

        private async Task InitializeUsers()
        {
            string eMailAddress = "peter@hogent.be";
            ApplicationUser user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "klant"));

            eMailAddress = "jan@gmail.com";
            user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "klant"));
        }
    }
}


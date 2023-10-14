using Microsoft.AspNetCore.Identity;

namespace MembershipsManagement.Entities
{
    public class Korisnik : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? DzematId { get; set; }
        public Dzemat? Dzemat { get; set; }
        public string FathersName { get; set; } = string.Empty;
        public List<Prihod>? ProknjizeniPrihodi { get; set; } = new List<Prihod>();
        public Pozicija Pozicija { get; set; }
    }
}

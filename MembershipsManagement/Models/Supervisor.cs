using MembershipsManagement.Entities;

namespace MembershipsManagement.Models
{
    public class Supervisor : Korisnik
    {
        public string? DzematId { get; set; }
        public Dzemat? Dzemat { get; set; }
        public List<Prihod>? ProknjizeniPrihodi { get; set; } = new List<Prihod>();
    }
}

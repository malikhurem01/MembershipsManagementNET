namespace MembershipsManagement.Entities
{
    public class Donator
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string? Adresa { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public List<Prihod> Prihodi = new List<Prihod>();
    }
}

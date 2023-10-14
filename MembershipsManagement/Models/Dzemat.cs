namespace MembershipsManagement.Entities
{
    public class Dzemat
    {
        public int Id { get; set; }
        public int MedzlisId { get; set; }
        public Medzlis Medzlis { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string? Lokacija { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public List<Clan>? Clanovi { get; set; } = new List<Clan>();
        public List<Korisnik>? Korisnici { get; set; } = new List<Korisnik>();
        public List<Baza>? Baze { get; set; } = new List<Baza>();
        public double? Budget { get; set; }
        
    }
}

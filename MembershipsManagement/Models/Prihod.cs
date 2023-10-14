namespace MembershipsManagement.Entities
{
    public class Prihod
    {
        public int Id { get; set; }
        public Dzemat Dzemat { get; set; } 
        public int ProjekatId { get; set; }
        public Projekat Projekat { get; set; }
        public double Iznos { get; set; }
        public string? Napomena { get; set; } = string.Empty;
        public DateTime DatumUplate { get; set; }
        public int DonatorId { get; set; }
        public Donator Donator { get; set; }
        public string KorisnikId { get; set; } = string.Empty;
        public Korisnik Korisnik { get; set; }
        public string? VrstaPrihoda { get; set; } = string.Empty;
    }
}

namespace MembershipsManagement.Entities
{
    public class Projekat
    {
        public int Id { get; set; }
        public bool Arhiva { get; set; }
        public Dzemat Dzemat { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string? Opis { get; set; } = string.Empty;
        public List<Prihod> Prihodi { get; set; } = new List<Prihod>();
        public DateTime Start { get; set; }
        public DateTime? Kraj { get; set; }
        public Baza Baza { get; set; }
        public double Budget { get; set; }

    }
}

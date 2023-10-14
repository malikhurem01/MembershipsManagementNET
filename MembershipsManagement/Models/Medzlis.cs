namespace MembershipsManagement.Entities
{
    public class Medzlis
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Lokacija { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public Korisnik? Korisnik { get; set; }
        public List<Dzemat>? Dzemati { get; set; } = new List<Dzemat>();
        
    }
}

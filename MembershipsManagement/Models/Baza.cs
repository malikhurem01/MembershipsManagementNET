namespace MembershipsManagement.Entities
{
    public class Baza
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public int Year { get; set; }
        public int DzematId { get; set; }
        public Dzemat Dzemat { get; set; }
        public List<Uplata>? Uplate { get; set; } = new List<Uplata>();
        public List<Clan>? Clanovi { get; set; } = new List<Clan>();
        public List<ClanInfo>? ClanInfo { get; set; } = new List<ClanInfo>();
        public List<Projekat>? Projekti { get; set; } = new List<Projekat>();
    }
}

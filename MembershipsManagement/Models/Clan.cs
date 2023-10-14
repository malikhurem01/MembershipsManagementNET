namespace MembershipsManagement.Entities
{
    public class Clan
    {
        public int Id { get; set; }
        public int EvBroj { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public Status Status { get; set; }
        public int IznosClanarine { get; set; }
        public List<Uplata>? Uplate { get; set; } = new List<Uplata>();
        public List<ClanPorodice>? FamilyMembers { get; set; } = new List<ClanPorodice>();
        public string? DzematId { get; set; }
        public Dzemat? Dzemat { get; set; }
    }
}

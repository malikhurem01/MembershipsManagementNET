namespace MembershipsManagement.Entities
{
    public class ClanPorodice
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public DateTime DatumRodjenja { get; set; }
        public PorodicaStatus Status { get; set; }
        public int ClanId { get; set; }
        public Clan Clan { get; set; }
    }
}

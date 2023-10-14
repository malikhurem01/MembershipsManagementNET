namespace MembershipsManagement.Entities
{
    public class ClanInfo
    {
        public int Id { get; set; }
        public int BazaId { get; set; }
        public Baza Baza { get; set; }
        public int ClanId { get; set; }
        public Clan Clan { get; set; }
        public bool ImaDug { get; set; }
        public string Napomena { get; set; } = string.Empty;
    }
}

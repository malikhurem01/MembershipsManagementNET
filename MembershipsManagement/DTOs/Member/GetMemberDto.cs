using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Member
{
    public class GetMemberDto
    {
        public string Ime { get; set; } = string.Empty;
        public string ImeOca { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int EvBroj { get; set; }
        public List<ClanPorodice> Domacinstvo { get; set; } = new List<ClanPorodice>();
        public List<Uplata> Uplate { get; set; } = new List<Uplata>();
        public ClanInfo? ClanInfo { get; set; }
        public int DzematId { get; set; }
    }
}

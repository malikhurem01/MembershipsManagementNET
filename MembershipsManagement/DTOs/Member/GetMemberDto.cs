using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Member
{
    public class GetMemberDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int EvBroj { get; set; }
        public List<ClanPorodice> FamilyMembers { get; set; } = new List<ClanPorodice>();
        public List<Uplata> Uplate { get; set; } = new List<Uplata>();
        public ClanInfo? ClanInfo { get; set; }
        public string DzematId { get; set; }
    }
}

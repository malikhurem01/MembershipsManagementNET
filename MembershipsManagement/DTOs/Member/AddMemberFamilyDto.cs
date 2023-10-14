using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Member
{
    public class AddMemberFamilyDto
    {
        public string Ime { get; set; } = string.Empty;
        public DateTime? DatumRodjenja { get; set; }
        public PorodicaStatus Status { get; set; }
    }
}

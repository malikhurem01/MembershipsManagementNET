using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Member
{
    public class ModifyMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int IznosClanarine { get; set; }
        public int EvBroj { get; set; }
        public int? DzematId { get; set; }
    }
}

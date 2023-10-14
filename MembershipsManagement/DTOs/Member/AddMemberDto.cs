using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Member
{
    public class AddMemberDto
    {
        public string Ime { get; set; } = string.Empty;
        public string ImeOca { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public Status Status { get; set; }
        public int IznosClanarine { get; set; }
        public int EvBroj { get; set; }
        public List<AddMemberFamilyListDto>? Domacinstvo { get; set; } = new List<AddMemberFamilyListDto>();
        public string? DzematId { get; set; }
    }
}

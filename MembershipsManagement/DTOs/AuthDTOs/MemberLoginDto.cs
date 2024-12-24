using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.AuthDTOs
{
    public class MemberLoginDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string InstitutionUserName { get; set; } = string.Empty;
    }
}

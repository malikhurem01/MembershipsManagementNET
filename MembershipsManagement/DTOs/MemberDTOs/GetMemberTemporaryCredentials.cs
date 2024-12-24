namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class GetMemberTemporaryCredentials
    {
        public string EvNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
        public string TemporaryPassword { get; set; } = string.Empty;
    }
}

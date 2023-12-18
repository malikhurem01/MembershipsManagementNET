namespace MembershipsManagement.DTOs.UserDTOs
{
    public class ModifyUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? FathersName { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? PlaceOfResidence { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}

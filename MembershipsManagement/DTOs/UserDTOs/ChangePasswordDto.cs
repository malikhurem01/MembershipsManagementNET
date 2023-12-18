namespace MembershipsManagement.DTOs.UserDTOs
{
    public class ChangePasswordDto
    {
        public string Id { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

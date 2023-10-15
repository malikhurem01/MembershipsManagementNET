namespace MembershipsManagement.DTOs.MedzlisDTOs
{
    public class ModifyMedzlisDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
    }
}

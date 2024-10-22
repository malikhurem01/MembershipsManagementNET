namespace MembershipsManagement.DTOs.MedzlisDTOs
{
    public class AddMedzlisDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int ZipCode { get; set; }
    }
}

namespace MembershipsManagement.DTOs.SpreadsheetDTOs
{
    public class ArchiveSpreadsheetDto
    {
        public int SpreadsheetId { get; set; }
        public int DzematId { get; set; }
        public string Password { get; set; } = string.Empty;
        public int SpreadsheetYear { get; set; }
    }
}

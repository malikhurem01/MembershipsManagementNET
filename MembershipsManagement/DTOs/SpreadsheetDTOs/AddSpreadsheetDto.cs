namespace MembershipsManagement.DTOs.BazaDTOs
{
    public class AddSpreadsheetDto
    {
        public string DzematId { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool Archived { get; set; } = false;
    }
}

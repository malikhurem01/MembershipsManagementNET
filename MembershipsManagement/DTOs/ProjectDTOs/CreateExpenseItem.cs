namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class CreateExpenseItem
    {
        public string Name { get; set; } = string.Empty;
        public int DzematId { get; set; }
        public int SpreadsheetId { get; set; }
    }
}

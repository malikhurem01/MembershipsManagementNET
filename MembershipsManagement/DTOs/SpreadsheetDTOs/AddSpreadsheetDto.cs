namespace MembershipsManagement.DTOs.BazaDTOs
{
    public class AddSpreadsheetDto
    {
        public string DzematId { get; set; } = string.Empty;
        public int Year { get; set; }
        public int MarriageFee { get; set; }
        public int WidowerFee { get; set; }
        public int AgeLimitFee { get; set; }
        public bool Archived { get; set; } = false;
    }
}

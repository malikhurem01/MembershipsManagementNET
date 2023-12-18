namespace MembershipsManagement.DTOs.SpreadsheetDTOs
{
    public class AddSpreadsheetDto
    {
        public int DzematId { get; set; }
        public int Year { get; set; }
        public int MarriageFee { get; set; }
        public int WidowerFee { get; set; }
        public int AgeLimitFee { get; set; }
        public bool Archived { get; set; } = false;
    }
}

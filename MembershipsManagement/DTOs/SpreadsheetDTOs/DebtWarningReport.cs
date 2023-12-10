namespace MembershipsManagement.DTOs.SpreadsheetDTOs
{
    public class DebtWarningReport
    {
        public string DzematName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get;  set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double Debt { get; set; }
    }
}

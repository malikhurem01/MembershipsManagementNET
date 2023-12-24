using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class AddIncomeDto
    {
        public int DzematId { get; set; }
        public int IncomeItemId { get; set; }
        public int? DonatorId { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}

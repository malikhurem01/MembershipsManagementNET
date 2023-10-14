using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public string DzematId { get; set; } = string.Empty;
        public Dzemat Dzemat { get; set; } 
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; } = string.Empty;
        public DateTime DateOfPayment { get; set; }
        public int DonatorId { get; set; }
        public Donator Donator { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public Supervisor Supervisor { get; set; }
        public string? IncomeType { get; set; } = string.Empty;
    }
}

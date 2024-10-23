using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public int IncomeItemId { get; set; }
        public IncomeItem IncomeItem { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public Supervisor Supervisor { get; set; }
        public int? DonatorId { get; set; }
        public Donator? Donator { get; set; }
        public int? MemberId { get; set; }
        public Member? Member { get; set; }
        public string? Note { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}

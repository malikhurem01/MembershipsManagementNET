using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public Supervisor Supervisor { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}

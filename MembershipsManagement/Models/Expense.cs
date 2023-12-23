using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public int ExpenseItemId { get; set; }
        public ExpenseItem ExpenseItem { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public Supervisor Supervisor { get; set; }
        public string? Note { get; set; } = string.Empty;
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

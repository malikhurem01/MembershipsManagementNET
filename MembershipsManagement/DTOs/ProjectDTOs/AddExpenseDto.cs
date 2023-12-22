namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class AddExpenseDto
    {
        public int DzematId { get; set; }
        public int ExpenseItemId { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}

namespace MembershipsManagement.Entities
{
    public class ExpenseItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
        public List<Expense>? Expenses { get; set; } = new List<Expense>();
        public double TotalExpenses 
        { 
            get 
            {
                double value = 0;
                if (Expenses is not null)
                    Expenses.ForEach(expense =>
                    {
                        value += expense.Amount;
                    });
                return value;
            } 
        }
    }
}

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetActiveExpenseItems
    {
        public List<GetExpenseItemDto> ExpenseItems { get; set; } = new List<GetExpenseItemDto>();
        public int SpreadsheetYear { get; set; }
        public int SpreadsheetId { get; set; }
        public List<List<double>> TotalExpensesAmountListed { get; set; }
        public double? TotalExpensesAmount
        {
            get
            {
                double value = 0;
                if (this.TotalExpensesAmountListed?.Count() > 0)
                {
                    this.TotalExpensesAmountListed.ForEach(p =>
                    {
                        if(p.Count > 0)
                        value += p.ElementAt(0);
                    });
                }
                return value;
            }
        }
    }
}

using MembershipsManagement.Entities;

namespace MembershipsManagement.Models
{
    public class IncomeItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
        public IncomeType IncomeType { get; set; }
        public List<Income>? Income { get; set; } = new List<Income>();
        public double TotalIncome
        {
            get
            {
                double value = 0;
                if (Income is not null)
                    Income.ForEach(income =>
                    {
                        value += income.Amount;
                    });
                return value;
            }
        }
    }
}

namespace MembershipsManagement.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public bool Archive { get; set; }
        public string DzematId { get; set; } = string.Empty;
        public Dzemat Dzemat { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Income>? Incomes { get; set; } = new List<Income>();
        public List<Expense>? Expenses { get; set; } = new List<Expense>();
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int SpreadSheetId { get; set; }
        public SpreadSheet SpreadSheet { get; set; }
        public double Budget { get; set; }

    }
}

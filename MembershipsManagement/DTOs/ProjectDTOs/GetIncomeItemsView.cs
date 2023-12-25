namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetIncomeItemsView
    {
        public List<GetIncomeItemDto> IncomeItems { get; set; } = new List<GetIncomeItemDto>();
        public int SpreadsheetYear { get; set; }
        public int SpreadsheetId { get; set; }
        public List<List<double>> TotalIncomeAmountListed { get; set; }
        public double? TotalIncomeAmount
        {
            get
            {
                double value = 0;
                if (this.TotalIncomeAmountListed?.Count() > 0)
                {
                    this.TotalIncomeAmountListed.ForEach(p =>
                    {
                        if (p.Count > 0)
                            value += p.ElementAt(0);
                    });
                }
                return value;
            }
        }
    }
}

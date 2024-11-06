namespace MembershipsManagement.Entities
{
    public class MemberInfo
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SpreadsheetId { get; set; }
        public Spreadsheet Spreadsheet { get; set; }
        public List<Payment>? Payments { get; set; } = new List<Payment>();
        public int MembershipFee
        {
            get
            {
                int value = -1;
                if (this.Member is not null && this.Spreadsheet is not null)
                {
                    switch (this.Member.Status)
                    {
                        case Status.Marriage:
                            value = this.Spreadsheet.MarriageFee;
                            break;
                        case Status.Widower:
                            value = this.Spreadsheet.WidowerFee;
                            break;
                        case Status.AgeLimit:
                            value = this.Spreadsheet.AgeLimitFee;
                            break;
                        case Status.NOT_SET:
                            value = 0;
                            break;
                    }
                }
                return value;
            }
        }

        public bool HasPayed
        {
            get
            {
                return this.TotalAmountPayed == this.MembershipFee;
            }
        }
        public double TotalAmountPayed
        {
            get
            {
                double value = 0;
                if (this.Payments is not null)
                {
                    this.Payments.ForEach(p =>
                    {
                        if (p.SpreadsheetId == this.SpreadsheetId)
                        {
                            value += p.Amount;
                        }
                    });
                }
                return value;
            }
        }
    }
}

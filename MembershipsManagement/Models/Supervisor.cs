using MembershipsManagement.Entities;

namespace MembershipsManagement.Models
{
    public class Supervisor : User
    {
        public int? DzematId { get; set; }
        public Dzemat? Dzemat { get; set; }
        public int? MedzlisId { get; set; }
        public Medzlis? Medzlis { get; set; }
        public List<Income>? RecordedIncomes { get; set; } = new List<Income>();
    }
}

using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Dug
    {
        public int Id { get; set; }
        public int DebtFee { get; set; }
        public Clan Clan { get; set; }
        public Supervisor Supervisor { get; set; }
    }
}

using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Uplata
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfPayment { get; set; }
        public int ClanId { get; set; }
        public Clan Clan { get; set; }
        public Supervisor Supervisor { get; set; }
        public Baza Baza { get; set; }
    }
}

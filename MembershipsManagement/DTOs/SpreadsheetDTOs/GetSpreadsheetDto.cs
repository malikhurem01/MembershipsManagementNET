using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.BazaDTOs
{
    public class GetSpreadsheetDto
    {
        public int Id { get; set; }
        public string DzematId { get; set; } = string.Empty;
        public bool Archived { get; set; }
        public int Year { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public List<Member> Members { get; set; } = new List<Member>();
        public List<Project> Projekti { get; set; } = new List<Project>();
    }
}

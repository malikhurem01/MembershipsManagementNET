using MembershipsManagement.Models;

namespace MembershipsManagement.Entities
{
    public class Medzlis
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public List<Supervisor>? Supervisors { get; set; } = new List<Supervisor>();
        public List<Dzemat>? Dzemati { get; set; } = new List<Dzemat>();
        
    }
}

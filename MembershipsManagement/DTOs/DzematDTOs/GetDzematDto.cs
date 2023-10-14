using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.DzematDTOs
{
    public class GetDzematDto
    {
        public string Id { get; set; } = string.Empty;
        public int MedzlisId { get; set; }
        public Medzlis Medzlis { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public List<Clan>? Members { get; set; } = new List<Clan>();
        public List<Supervisor>? Supervisors { get; set; } = new List<Supervisor>();
        public List<Baza>? Baze { get; set; } = new List<Baza>();
        public double? Budget { get; set; }
    }
}

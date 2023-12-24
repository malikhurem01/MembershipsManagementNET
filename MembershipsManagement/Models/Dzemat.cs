using MembershipsManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace MembershipsManagement.Entities
{
    public class Dzemat
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int MedzlisId { get; set; }
        public Medzlis Medzlis { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public List<Member>? Members { get; set; } = new List<Member>();
        public List<Supervisor>? Supervisors { get; set; } = new List<Supervisor>();
        public List<Spreadsheet>? Spreadsheets { get; set; } = new List<Spreadsheet>();
        public List<Donator>? Donators { get; set; } = new List<Donator>();
        public double? Budget { get; set; }
        
    }
}

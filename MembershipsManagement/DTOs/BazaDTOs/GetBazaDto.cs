using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.BazaDTOs
{
    public class GetBazaDto
    {
        public int Id { get; set; }
        public string DzematId { get; set; } = string.Empty;
        public bool Archived { get; set; }
        public int Year { get; set; }
        public List<Uplata> Uplate { get; set; } = new List<Uplata>();
        public List<Clan> Clanovi { get; set; } = new List<Clan>();
        public List<Projekat> Projekti { get; set; } = new List<Projekat>();
    }
}

using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Baza
{
    public class GetBazaDto
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public int Year { get; set; }
        public List<Uplata> Uplate { get; set; } = new List<Uplata>();
        public List<Clan> Clanovi { get; set; } = new List<Clan>();
        public List<Projekat> Projekti { get; set; } = new List<Projekat>();
    }
}

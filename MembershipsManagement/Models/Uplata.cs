namespace MembershipsManagement.Entities
{
    public class Uplata
    {
        public int Id { get; set; }
        public double Iznos { get; set; }
        public DateTime Datum { get; set; }
        public int ClanId { get; set; }
        public Clan Clan { get; set; }
        public Korisnik Nadlezni { get; set; }
        public Baza Baza { get; set; }
    }
}

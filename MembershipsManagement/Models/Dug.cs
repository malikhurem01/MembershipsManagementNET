namespace MembershipsManagement.Entities
{
    public class Dug
    {
        public int Id { get; set; }
        public int IznosDuga { get; set; }
        public Clan Clan { get; set; }
        public Korisnik Nadlezni { get; set; }
    }
}

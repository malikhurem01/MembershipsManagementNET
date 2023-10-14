namespace MembershipsManagement.Entities
{
    public class Trosak
    {
        public int Id { get; set; }
        public Dzemat Dzemat { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public double Iznos { get; set; }
    }
}

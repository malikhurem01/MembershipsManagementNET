namespace MembershipsManagement.Entities
{
    public class Trosak
    {
        public int Id { get; set; }
        public string DzematId { get; set; } = string.Empty;
        public Dzemat Dzemat { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}

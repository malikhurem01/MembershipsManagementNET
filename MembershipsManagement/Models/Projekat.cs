namespace MembershipsManagement.Entities
{
    public class Projekat
    {
        public int Id { get; set; }
        public bool Archive { get; set; }
        public string DzematId { get; set; } = string.Empty;
        public Dzemat Dzemat { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Prihod> Incomes { get; set; } = new List<Prihod>();
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public Baza Baza { get; set; }
        public double Budget { get; set; }

    }
}

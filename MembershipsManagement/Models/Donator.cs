namespace MembershipsManagement.Entities
{
    public class Donator
    {
        public int Id { get; set; }
        public int DzematId { get; set; }
        public Dzemat Dzemat { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public List<Income>? Donations = new List<Income>();
    }
}

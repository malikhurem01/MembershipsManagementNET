using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetDonatorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public List<GetIncomeDto> Donations = new List<GetIncomeDto>();
    }
}

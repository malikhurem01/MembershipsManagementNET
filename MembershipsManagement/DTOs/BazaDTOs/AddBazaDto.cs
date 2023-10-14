namespace MembershipsManagement.DTOs.BazaDTOs
{
    public class AddBazaDto
    {
        public string DzematId { get; set; } = string.Empty;
        public int Year;
        public bool Archived = false;
    }
}

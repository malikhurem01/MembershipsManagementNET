using Microsoft.AspNetCore.Identity;

namespace MembershipsManagement.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set;} = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public Roles Position { get; set; }
    }
}

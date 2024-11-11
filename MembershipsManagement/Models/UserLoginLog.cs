using MembershipsManagement.Entities;

namespace MembershipsManagement.Models
{
    public class UserLoginLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DzematName { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public string DeviceInfo { get; set; } = string.Empty;
        public User User { get; set; }
    }

}

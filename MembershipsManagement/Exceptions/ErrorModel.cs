using Newtonsoft.Json;

namespace MembershipsManagement.Exceptions
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

namespace Aggregate.Intellegence.Library.Web.Service.Models
{
    public class AuthenticationResponce
    {
        public string JwtToken { get; set; }
        public bool ValidUser { get; set; }
        public bool ValidPassword { get; set; }
        public bool IsActive { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}

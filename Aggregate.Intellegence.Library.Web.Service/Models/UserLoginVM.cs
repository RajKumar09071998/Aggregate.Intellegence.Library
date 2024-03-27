using System.ComponentModel.DataAnnotations;

namespace Aggregate.Intellegence.Library.Web.Service.Models
{
    public class UserLoginVM
    {
        public string username { get; set; }
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}

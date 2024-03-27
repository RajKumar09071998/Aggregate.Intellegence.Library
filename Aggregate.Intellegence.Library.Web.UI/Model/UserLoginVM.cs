using System.ComponentModel.DataAnnotations;

namespace Aggregate.Intellegence.Library.Web.UI.Model
{
    public class UserLoginVM
    {
        [Required]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}

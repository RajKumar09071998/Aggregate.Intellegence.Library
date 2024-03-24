using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregate.Intellegence.Library.Web.Service.Models
{
    [Table("User")]
    public class User:Common
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? RoleId { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
    }
}

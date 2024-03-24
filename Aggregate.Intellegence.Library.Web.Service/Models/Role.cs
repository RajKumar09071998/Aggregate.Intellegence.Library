using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregate.Intellegence.Library.Web.Service.Models
{
    [Table("Role")]
    public class Role:Common
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}

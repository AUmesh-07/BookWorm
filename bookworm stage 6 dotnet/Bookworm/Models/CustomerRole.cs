using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Bookworm.Models;

namespace Bookworm.Models
{
    [Table("customer_roles")]
    public class CustomerRole
    {
        [Key]
        [Column("customer_id", Order = 1)]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Key]
        [Column("role_id", Order = 2)]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}

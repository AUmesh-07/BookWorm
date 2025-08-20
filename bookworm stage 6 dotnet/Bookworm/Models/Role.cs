using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Models
{
    [Table("roles")]
    [Index(nameof(Name), IsUnique = true)]
    public class Role 
    {
        [Key]
        [Column("role_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("name")]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<CustomerRole> CustomerRoles { get; set; } = new HashSet<CustomerRole>();
    }
}
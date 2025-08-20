using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Bookworm.Models
{
    [Table("cart_master")]
    public class Cart
    {
        [Key]
        [Column("cart_id")]
        public int Id { get; set; }

        [Required]
        [Column("customer_id")]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Column("cost", TypeName = "decimal(10, 2)")]
        public decimal? Cost { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        // One-to-many relationship with CartDetail
        public virtual ICollection<CartDetail> CartDetails { get; set; } = new HashSet<CartDetail>();
    }

}
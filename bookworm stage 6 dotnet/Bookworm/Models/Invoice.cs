using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Bookworm.Models
{
    [Table("invoice_master")]
    public class Invoice
    {
        [Key]
        [Column("invoice_id")]
        public long InvoiceId { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Column("cart_id")]
        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public virtual Cart Cart { get; set; }

        [Required]
        [Column("customer_id")]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        [Column("date" ,TypeName = "date") ]
        public DateTime Date { get; set; }

        // Navigation property for one-to-many relationship with InvoiceDetail
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

        // Navigation property for one-to-many relationship with RoyaltyCalculation
        public virtual ICollection<RoyaltyCalculation> RoyaltyCalculations { get; set; } = new List<RoyaltyCalculation>();
    }
}
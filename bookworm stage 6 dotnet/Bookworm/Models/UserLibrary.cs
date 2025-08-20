using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Models
{
    // ... Other entities remain the same ...

    /// <summary>
    /// Represents a customer's entitlement to a product.
    /// Maps to the 'user_library' table.
    /// </summary>
    [Table("user_library")]
    [Index(nameof(InvoiceDetailId), IsUnique = true)]
    public class UserLibrary
    {
        [Key]
        [Column("user_library_id")]
        public long UserLibraryId { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("invoice_detail_id")]
        public int InvoiceDetailId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("acquisition_type")]
        public string AcquisitionType { get; set; }

        [Required]
        [Column("acquisition_timestamp")]
        public DateTime AcquisitionTimestamp { get; set; }

        [Required]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; }

        // Navigation properties for foreign key relationships
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public virtual InvoiceDetail InvoiceDetail { get; set; }

        // One-to-one relationship with RentalLedger
        public virtual RentalLedger RentalLedger { get; set; }
    }
}
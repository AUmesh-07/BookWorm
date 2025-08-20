using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Bookworm.Models
{
    [Table("invoice_details")]
    public class InvoiceDetail
    {
        [Key]
        [Column("inv_dtl_id")]
        public int InvDtlId { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("rent_no_of_days")]
        public int? RentNoOfDays { get; set; }

        [Required]
        [Column("royalty_amount", TypeName = "decimal(10, 2)")]
        public decimal RoyaltyAmount { get; set; }

        [Required]
        [Column("sell_price", TypeName = "decimal(10, 2)")]
        public decimal SellPrice { get; set; }

        [Required]
        [StringLength(20)]
        [Column("tran_type")]
        public string TranType { get; set; }

        // Foreign key properties
        [Required]
        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Column("invoice_id")]
        [ForeignKey("Invoice")]
        public long InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        // One-to-one relationship with UserLibrary
        public virtual UserLibrary UserLibrary { get; set; }
    }
}
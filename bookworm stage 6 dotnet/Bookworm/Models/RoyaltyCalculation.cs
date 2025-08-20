using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Bookworm.Models
{
    [Table("royalty_calculation")]
    public class RoyaltyCalculation
    {
        [Key]
        [Column("royalty_id")]
        public int RoyaltyId { get; set; }

        [Required]
        [Column("royalty_date")]
        public DateTime RoyaltyDate { get; set; }

        [Required]
        [StringLength(20)]
        [Column("transaction_type")]
        public string TransactionType { get; set; }

        [Required]
        [Column("sales_price", TypeName = "decimal(10, 2)")]
        public decimal SalesPrice { get; set; }

        [Required]
        [Column("royalty_on_sales_price", TypeName = "decimal(10, 2)")]
        public decimal RoyaltyOnSalesPrice { get; set; }

        [Required]
        [Column("beneficiary_id")]
        [ForeignKey("BeneficiaryMaster")]
        public int BeneficiaryId { get; set; }
        public virtual BeneficiaryMaster Beneficiary { get; set; }

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
    }
}
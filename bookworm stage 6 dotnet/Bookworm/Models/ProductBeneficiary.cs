using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bookworm.Models
{
    [Table("product_beneficiary")]
    public class ProductBeneficiary
    {
        [Key]
        [Column("product_beneficiary_id")]
        public int Id { get; set; }

        [Required]
        [Column("percentage", TypeName = "decimal(10, 2)")]
        public decimal Percentage { get; set; }

        [Required]
        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Column("beneficiary_id")]
        [ForeignKey("BeneficiaryMaster")]
        public int BeneficiaryId { get; set; }
        public virtual BeneficiaryMaster Beneficiary { get; set; }
    }
}
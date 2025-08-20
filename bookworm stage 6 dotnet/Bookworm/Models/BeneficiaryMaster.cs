using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Models
{
    [Table("beneficiary_master")]
    [Index(nameof(BenEmail), IsUnique = true)]
    [Index(nameof(BenPan), IsUnique = true)]
    public class BeneficiaryMaster
    {
        [Key]
        [Column("ben_id")]
        public int BenId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("ben_name")]
        public string BenName { get; set; }

        [Required]
        [StringLength(255)]
        [Column("ben_email")]
        public string BenEmail { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ben_pan")]
        public string BenPan { get; set; }

        // Navigation property for one-to-many relationship with ProductBeneficiary
        public virtual ICollection<ProductBeneficiary> ProductBeneficiaries { get; set; } = new HashSet<ProductBeneficiary>();

        // Navigation property for one-to-many relationship with RoyaltyCalculation
        public virtual ICollection<RoyaltyCalculation> RoyaltyCalculations { get; set; } = new HashSet<RoyaltyCalculation>();
    }

}
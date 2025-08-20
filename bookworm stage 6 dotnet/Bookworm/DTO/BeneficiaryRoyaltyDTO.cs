using System.ComponentModel.DataAnnotations.Schema;

namespace Bookworm.RequestDTO
{
    public class BeneficiaryRoyaltyDTO
    {
        public int BeneficiaryId { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Percentage { get; set; }
    }

}

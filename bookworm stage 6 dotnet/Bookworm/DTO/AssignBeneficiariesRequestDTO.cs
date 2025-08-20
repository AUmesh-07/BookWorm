namespace Bookworm.RequestDTO
{
    public class AssignBeneficiariesRequestDTO
    {
        public int ProductId { get; set; }
        public List<BeneficiaryRoyaltyDTO> Beneficiaries { get; set; }
    }

    public class BeneficiaryRoyaltyDto
    {
        public int BeneficiaryId { get; set; }
        public decimal Percentage { get; set; }
    }
}

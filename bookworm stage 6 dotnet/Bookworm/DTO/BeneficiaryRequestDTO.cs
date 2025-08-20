using System.Text.Json.Serialization;

namespace Bookworm.RequestDTO
{
    public class BeneficiaryRequestDTO
    {
        [JsonPropertyName("benName")]
        public string BenName { get; set; }

        [JsonPropertyName("benEmail")]
        public string BenEmail { get; set; }

        [JsonPropertyName("benPan")]
        public string BenPan { get; set; }
    }
}
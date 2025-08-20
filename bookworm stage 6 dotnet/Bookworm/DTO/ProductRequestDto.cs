using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookworm.DTO
{
    public class ProductRequestDto
    {
        // [Required] and other validation attributes can be added here
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ImageSource { get; set; }
        public bool IsRentable { get; set; }
        public int? MinRentDays { get; set; }
        public decimal? RentPerDay { get; set; }
        public decimal BasePrice { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal? SpecialCost { get; set; }
        public int GenreId { get; set; }
        public int LanguageId { get; set; }
        public int ProductTypeId { get; set; }
    }
}
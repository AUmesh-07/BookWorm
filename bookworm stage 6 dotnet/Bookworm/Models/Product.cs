using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Models
{
    [Table("product_master")]
    [Index(nameof(Isbn), IsUnique = true)]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("product_author")]
        public string Author { get; set; }

        [Required]
        [Column("product_base_price", TypeName = "decimal(10, 2)")]
        public decimal BasePrice { get; set; }

        [StringLength(255)]
        [Column("english_name")]
        public string EnglishName { get; set; }

        [StringLength(255)]
        [Column("img_src")]
        public string ImageSource { get; set; }

        [Column("is_rentable")]
        public bool? IsRentable { get; set; }

        [StringLength(20)]
        [Column("product_isbn")]
        public string Isbn { get; set; }

        [Column("long_description", TypeName = "TEXT")]
        public string LongDescription { get; set; }

        [Column("min_rent_days")]
        public int? MinRentDays { get; set; }

        [Required]
        [StringLength(255)]
        [Column("product_name")]
        public string Name { get; set; }

        [Column("product_offer_price", TypeName = "decimal(10, 2)")]
        public decimal? OfferPrice { get; set; }

        [Column("rent_per_day", TypeName = "decimal(10, 2)")]
        public decimal? RentPerDay { get; set; }

        [Column("product_sp_cost", TypeName = "decimal(10, 2)")]
        public decimal? SpecialCost { get; set; }

        [StringLength(255)]
        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("genre_id")]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [Required]
        [Column("language_id")]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        [Required]
        [Column("type_id")]
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

        // Navigation property for one-to-many relationship with ProductBeneficiary
        public virtual ICollection<ProductBeneficiary> ProductBeneficiaries { get; set; } = new List<ProductBeneficiary>();

        public virtual ICollection<CartDetail> CartDetails { get; set; }

    }

}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Bookworm.Models
{
    [Table("cart_details")]
    public class CartDetail
    {
        [Key]
        [Column("cart_details_id")]
        public int Id { get; set; }

        [Column("is_rented")]
        public bool IsRented { get; set; }

        [Column("is_updated")]
        public bool? IsUpdated { get; set; }

        [Required]
        [Column("offer_cost", TypeName = "decimal(10, 2)")]
        public decimal OfferCost { get; set; }

        [Column("rent_no_of_days")]
        public int? RentNumberOfDays { get; set; }

        [Required]
        [Column("cart_id")]
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        [Required]
        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
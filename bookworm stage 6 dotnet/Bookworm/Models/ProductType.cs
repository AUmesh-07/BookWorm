using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bookworm.Models
{
    /// </summary>
    [Table("product_type_master")]
    public class ProductType
    {
        [Key]
        [Column("type_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("type_desc")]
        public string Description { get; set; }
    }
}
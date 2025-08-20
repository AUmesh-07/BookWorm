using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bookworm.Models
{
    [Table("genre_master")]
    public class Genre
    {
        [Key]
        [Column("genre_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        [Column("genre_desc")]
        public string Description { get; set; }
    }
}
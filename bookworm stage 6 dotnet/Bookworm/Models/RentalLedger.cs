using Bookworm.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace Bookworm.Models
{
    [Table("rental_ledger")]
    public class RentalLedger
    {
        [Key]
        [Column("user_library_id")]
        public long UserLibraryId { get; set; }
        public virtual UserLibrary UserLibrary { get; set; }

        [Required]
        [Column("rent_start_date")]
        public DateTime RentStartDate { get; set; }

        [Required]
        [Column("rent_end_date")]
        public DateTime RentEndDate { get; set; }

        [Column("last_status_check")]
        public DateTime? LastStatusCheck { get; set; }
    }
}
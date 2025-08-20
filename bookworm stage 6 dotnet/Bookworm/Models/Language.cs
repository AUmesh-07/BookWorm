using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bookworm.Models
{
    [Table("language_master")]
    public class Language
    {
        [Key]
        [Column("language_id")]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("language_desc")]
        public string? Description { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;


namespace Bookworm.Models
{
    [Table("customer_master")]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class Customer 
    {
        [Key]
        [Column("customer_id")]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("address")]
        public string? Address { get; set; }

        [Column("age")]
        public int? Age { get; set; }

        [Column("dob", TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [StringLength(15)]
        [Column("phone")]
        public string? Phone { get; set; }

        // Navigation property for one-to-many relationship with Invoice
        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        // Many-to-many relationship with Role via the CustomerRole join entity
        public virtual ICollection<CustomerRole> CustomerRoles { get; set; } = new HashSet<CustomerRole>();

        // Navigation property for one-to-many relationship with Cart
        public virtual ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
    }
}
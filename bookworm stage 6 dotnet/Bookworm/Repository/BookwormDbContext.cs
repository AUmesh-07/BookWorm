using Microsoft.EntityFrameworkCore;
using Bookworm.Models;

namespace Bookworm.Repository
{
    public class BookwormDbContext : DbContext
    {
        public BookwormDbContext(DbContextOptions<BookwormDbContext> options)
            : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<BeneficiaryMaster> BeneficiaryMasters { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRole> CustomerRoles { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBeneficiary> ProductBeneficiaries { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<RentalLedger> RentalLedgers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoyaltyCalculation> RoyaltyCalculations { get; set; }
        public DbSet<UserLibrary> UserLibraries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: Customer and Role
            modelBuilder.Entity<CustomerRole>()
                .HasKey(cr => new { cr.CustomerId, cr.RoleId });
            modelBuilder.Entity<CustomerRole>()
                .HasOne(cr => cr.Customer)
                .WithMany(c => c.CustomerRoles)
                .HasForeignKey(cr => cr.CustomerId);
            modelBuilder.Entity<CustomerRole>()
                .HasOne(cr => cr.Role)
                .WithMany(r => r.CustomerRoles)
                .HasForeignKey(cr => cr.RoleId);

            // One-to-Many: Customer to Cart
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany(cust => cust.Carts)
                .HasForeignKey(c => c.CustomerId);

            // One-to-Many: Cart to CartDetail
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartId);

            // One-to-Many: Product to CartDetail
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Product)
                .WithMany(p => p.CartDetails)
                .HasForeignKey(cd => cd.ProductId);

            // One-to-Many: Product -> Genre, Language, ProductType
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Genre)
                .WithMany()
                .HasForeignKey(p => p.GenreId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Language)
                .WithMany()
                .HasForeignKey(p => p.LanguageId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);

            // One-to-Many: Product to ProductBeneficiary
            modelBuilder.Entity<ProductBeneficiary>()
                .HasOne(pb => pb.Product)
                .WithMany(p => p.ProductBeneficiaries)
                .HasForeignKey(pb => pb.ProductId);

            // One-to-Many: BeneficiaryMaster to ProductBeneficiary
            modelBuilder.Entity<ProductBeneficiary>()
                .HasOne(pb => pb.Beneficiary)
                .WithMany(b => b.ProductBeneficiaries)
                .HasForeignKey(pb => pb.BeneficiaryId);

            // One-to-One: UserLibrary and RentalLedger (Shared Primary Key)
            modelBuilder.Entity<RentalLedger>()
                .HasOne(rl => rl.UserLibrary)
                .WithOne(ul => ul.RentalLedger)
                .HasForeignKey<RentalLedger>(rl => rl.UserLibraryId);

            // One-to-Many: BeneficiaryMaster to RoyaltyCalculation
            modelBuilder.Entity<RoyaltyCalculation>()
                .HasOne(rc => rc.Beneficiary)
                .WithMany(b => b.RoyaltyCalculations)
                .HasForeignKey(rc => rc.BeneficiaryId);

            // One-to-Many: Product to RoyaltyCalculation
            modelBuilder.Entity<RoyaltyCalculation>()
                .HasOne(rc => rc.Product)
                .WithMany()
                .HasForeignKey(rc => rc.ProductId);

            // One-to-Many: Invoice to RoyaltyCalculation
            modelBuilder.Entity<RoyaltyCalculation>()
                .HasOne(rc => rc.Invoice)
                .WithMany(i => i.RoyaltyCalculations)
                .HasForeignKey(rc => rc.InvoiceId);

            // One-to-Many: Invoice to InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(id => id.Invoice)
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(id => id.InvoiceId);

            // One-to-Many: Product to InvoiceDetail
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(id => id.Product)
                .WithMany()
                .HasForeignKey(id => id.ProductId);

            // One-to-Many: Customer to Invoice
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId);

            // One-to-Many: Cart to Invoice
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Cart)
                .WithMany()
                .HasForeignKey(i => i.CartId);

            // One-to-One: InvoiceDetail to UserLibrary
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(id => id.UserLibrary)
                .WithOne(ul => ul.InvoiceDetail)
                .HasForeignKey<UserLibrary>(ul => ul.InvoiceDetailId);
        }
    }
}
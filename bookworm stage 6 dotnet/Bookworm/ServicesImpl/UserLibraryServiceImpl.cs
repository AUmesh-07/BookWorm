using Bookworm.Models;
using Bookworm.Repositories;
using Bookworm.Repository;
using Bookworm.ResponseDTO;
using Bookworm.Services;

namespace Bookworm.ServicesImpl
{
    public class UserLibraryService : IUserLibraryService
    {
        private readonly IUserLibraryRepository _userLibraryRepository;
        private readonly IRentalLedgerRepository _rentalLedgerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

       
        public UserLibraryService(IUserLibraryRepository userLibraryRepository,
                                  IRentalLedgerRepository rentalLedgerRepository,
                                  IProductRepository productRepository,
                                  IInvoiceDetailRepository invoiceDetailRepository)
        {
            _userLibraryRepository = userLibraryRepository;
            _rentalLedgerRepository = rentalLedgerRepository;
            _productRepository = productRepository;
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task<ShelfResponseDTO> GetShelfForCustomerAsync(int customerId)
        {
            // Find all active purchased entitlements using an asynchronous repository method.
            var purchasedEntries = await _userLibraryRepository.FindByCustomerIdAndAcquisitionTypeAndStatusAsync(
                customerId, "PURCHASE", "ACTIVE");

            // Convert each entity into a DTO using a mapping function.
            var shelfItems = purchasedEntries
                .Select(entry => MapToShelfItemDTOAsync(entry).Result) // .Result is used here for simplicity, but a better approach would be to await all tasks.
                .ToList();

            // Calculate the total price using LINQ's Sum method.
            var totalPrice = shelfItems.Sum(item => item.Price);

            // Assemble and return the final response.
            var response = new ShelfResponseDTO
            {
                Items = shelfItems,
                TotalQuantity = shelfItems.Count,
                TotalPrice = totalPrice
            };

            return response;
        }

        public async Task<LibraryResponseDTO> GetLibraryForCustomerAsync(int customerId)
        {
            // Find all active rental entitlements.
            var rentedEntries = await _userLibraryRepository.FindByCustomerIdAndAcquisitionTypeAndStatusAsync(
                customerId, "RENTAL", "ACTIVE");

            // Convert each entity into a DTO.
            var libraryItems = rentedEntries
                .Select(entry => MapToLibraryItemDTOAsync(entry).Result)
                .ToList();

            // Assemble and return the final response.
            var response = new LibraryResponseDTO
            {
                Items = libraryItems,
                TotalItems = libraryItems.Count
            };

            return response;
        }

        // Helper method to map a UserLibrary entity to a ShelfItemDTO.
        private async Task<ShelfItemDTO> MapToShelfItemDTOAsync(UserLibrary entry)
        {
            var product = await GetProductOrThrowAsync(entry.ProductId);

            var invoiceDetail = await _invoiceDetailRepository.FindByIdAsync(entry.InvoiceDetailId)
    ?? throw new InvalidOperationException($"Data integrity error: InvoiceDetail not found for ID {entry.InvoiceDetailId}");


            return new ShelfItemDTO
            {
                ProductId = entry.ProductId,
                ProductName = product.Name,
                Author = product.Author, // Assuming `Author` and `ImageSource` properties exist on the Product model.
                ImageSource = product.ImageSource,
                PurchaseDate = entry.AcquisitionTimestamp,
                Price = invoiceDetail.SellPrice
            };
        }

        // Helper method to map a UserLibrary entity to a LibraryItemDTO.
        private async Task<LibraryItemDTO> MapToLibraryItemDTOAsync(UserLibrary entry)
        {
            var product = await GetProductOrThrowAsync(entry.ProductId);

            var dto = new LibraryItemDTO
            {
                ProductId = entry.ProductId,
                ProductName = product.Name,
                Author = product.Author,
                ImageSource = product.ImageSource,
                RentalDate = entry.AcquisitionTimestamp
            };

            // Find the rental details and set the expiry date if it exists.
            var ledger = await _rentalLedgerRepository.FindByIdAsync(entry.UserLibraryId);
            dto.RentalExpiryDate = ledger?.RentEndDate;

            return dto;
        }

        // Helper to find a product by ID or throw a consistent error.
        private async Task<Product> GetProductOrThrowAsync(int productId)
        {
            return await _productRepository.GetById(productId)
                ?? throw new InvalidOperationException($"Data integrity error: Product not found for ID {productId}");
        }
    }
}

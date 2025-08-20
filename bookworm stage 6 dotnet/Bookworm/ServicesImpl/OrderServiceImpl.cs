using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookworm.Models;
using Bookworm.Repositories;
using Bookworm.Repository;
using Bookworm.RequestDTO;
using Bookworm.ResponseDTO;
using Bookworm.Services;

// Assume other entities and repository interfaces are defined in a separate file.
// For example: IInvoiceRepository, ICustomerRepository, IProductRepository, etc.

namespace Bookworm.OrderService
{
    // C# implementation of your Java OrderServiceImpl
    public class OrderService : IOrderService
    {
        // Repositories are injected via the constructor, replacing Lombok's @RequiredArgsConstructor.
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUserLibraryRepository _userLibraryRepository;
        private readonly IRentalLedgerRepository _rentalLedgerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IRoyaltyCalculationService _royaltyCalculationService;
        // --- 1. ADD THESE TWO NEW FIELDS ---
        // These fields will hold the services for creating the PDF and sending the email.
        private readonly IPdfInvoiceService _pdfInvoiceService;
        private readonly IEmailSender _emailSender;

        public OrderService(IInvoiceRepository invoiceRepository,
                            IUserLibraryRepository userLibraryRepository,
                            IRentalLedgerRepository rentalLedgerRepository,
                            IProductRepository productRepository,
                            ICustomerRepository customerRepository,
                            ICartRepository cartRepository,
                            IRoyaltyCalculationService royaltyCalculationService,
                            IPdfInvoiceService pdfInvoiceService, // Add this
                            IEmailSender emailSender)
        {
            _invoiceRepository = invoiceRepository;
            _userLibraryRepository = userLibraryRepository;
            _rentalLedgerRepository = rentalLedgerRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _cartRepository = cartRepository;
            _royaltyCalculationService = royaltyCalculationService;
            // Assign the injected services to your private fields.
            _pdfInvoiceService = pdfInvoiceService;
            _emailSender = emailSender;
        }

        // Method to create an order directly from a customer's cart.
        public async Task<OrderResponseDTO> CreateOrderFromCartAsync(int customerId)
        {
            // Use the null-coalescing operator and exception handling similar to Java's orElseThrow.
            var activeCart = await _cartRepository.FindByCustomerIdAndIsActiveTrue(customerId)
                ?? throw new InvalidOperationException($"No active cart found for customer ID: {customerId}");

            if (activeCart.CartDetails == null || !activeCart.CartDetails.Any())
            {
                throw new InvalidOperationException("Cannot create an order from an empty cart.");
            }

            // Use C# LINQ to transform cart details into DTOs.
            var orderItems = activeCart.CartDetails
                .Select(cartDetail => new OrderItemRequestDTO
                {
                    ProductId = cartDetail.Product.Id,
                    AcquisitionType = cartDetail.IsRented ? "RENTAL" : "PURCHASE",
                    RentalPeriodDays = cartDetail.RentNumberOfDays // `int` is implicitly converted to `int?`.
                })
                .ToList();

            var orderRequest = new OrderRequestDTO
            {
                CustomerId = customerId,
                Items = orderItems
            };

            // Call the main order creation logic.
            var createdOrder = await CreateOrderAsync(orderRequest);

            // Deactivate the cart after successful checkout.
            activeCart.IsActive = false;
            await _cartRepository.Save(activeCart);

            return createdOrder;
        }

        // The main order creation logic.
        public async Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO orderRequest)
        {
            // Find customer or throw an exception.
            var customer = await _customerRepository.GetById(orderRequest.CustomerId)
                ?? throw new InvalidOperationException($"Customer not found with ID: {orderRequest.CustomerId}");

            var invoice = new Invoice
            {
                CustomerId = orderRequest.CustomerId,
                Date = DateTime.Now // <-- ADDED: Initialize the Date property
            };

            var totalAmount = decimal.Zero;
            var invoiceDetails = new List<InvoiceDetail>();

            foreach (var itemDto in orderRequest.Items)
            {
                var product = await _productRepository.GetById(itemDto.ProductId)
                    ?? throw new InvalidOperationException($"Product not found with ID: {itemDto.ProductId}");

                var detail = new InvoiceDetail
                {
                    Product = product,
                    Quantity = 1,
                    Invoice = invoice
                };

                decimal itemPrice;

                if (string.Equals(itemDto.AcquisitionType, "SALE", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(itemDto.AcquisitionType, "PURCHASE", StringComparison.OrdinalIgnoreCase))
                {
                    detail.TranType = "SALE";
                    itemPrice = (product.OfferPrice.HasValue && product.OfferPrice.Value > 0)
                        ? product.OfferPrice.Value
                        : product.BasePrice;
                }
                else if (string.Equals(itemDto.AcquisitionType, "RENT", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(itemDto.AcquisitionType, "RENTAL", StringComparison.OrdinalIgnoreCase))
                {
                    // `RentalPeriodDays` is a nullable int?, so a null check is necessary.
                    if (!(product.IsRentable ?? false) || product.RentPerDay == null || itemDto.RentalPeriodDays <= 0)

                    {
                        throw new InvalidOperationException($"Product '{product.Name}' is not rentable or rental period is invalid.");
                    }
                    detail.TranType = "RENT";
                    detail.RentNoOfDays = itemDto.RentalPeriodDays.Value;
                    itemPrice = product.RentPerDay.Value * itemDto.RentalPeriodDays.Value;
                }
                else
                {
                    throw new InvalidOperationException($"Invalid acquisition type: {itemDto.AcquisitionType}");
                }

                detail.SellPrice = itemPrice;
                detail.RoyaltyAmount = decimal.Zero;
                totalAmount += itemPrice;
                invoiceDetails.Add(detail);
            }

            invoice.Amount = totalAmount;
            invoice.InvoiceDetails = invoiceDetails;

            var savedInvoice = await _invoiceRepository.SaveAsync(invoice);

            // Call the royalty calculation service.
            await _royaltyCalculationService.CalculateRoyaltyForInvoiceAsync(savedInvoice);
            Console.WriteLine("🎯 Calling royalty calculation...");

            foreach (var detail in savedInvoice.InvoiceDetails)
            {
                var libraryEntry = new UserLibrary
                {
                    CustomerId = savedInvoice.CustomerId,
                    ProductId = detail.Product.Id,
                    InvoiceDetailId = detail.InvDtlId,
                    Status = "ACTIVE",
                    AcquisitionType = string.Equals(detail.TranType, "SALE", StringComparison.OrdinalIgnoreCase) ? "PURCHASE" : "RENTAL",
                    AcquisitionTimestamp = DateTime.Now // <-- ADDED: Initialize the acquisition_timestamp property
                };

                var savedLibraryEntry = await _userLibraryRepository.SaveAsync(libraryEntry);

                if (string.Equals(libraryEntry.AcquisitionType, "RENTAL", StringComparison.OrdinalIgnoreCase))
                {
                    var ledger = new RentalLedger
                    {
                        UserLibrary = savedLibraryEntry,
                        RentStartDate = DateTime.UtcNow,
                        RentEndDate = DateTime.UtcNow.AddDays((double)detail.RentNoOfDays)
                    };
                    await _rentalLedgerRepository.SaveAsync(ledger);
                }
            }

            // --- 3. TRIGGER THE EMAIL SENDING PROCESS ---
            // After all database operations are successful, generate and send the email.
            // Note: This uses a different PDF service than the test controller. This one should
            // ideally take the 'savedInvoice' object to create a PDF from real data.
            // For now, we'll use the dummy data service to get it working.
            byte[] pdfBytes = _pdfInvoiceService.CreateInvoicePdf(savedInvoice);

            // Create the email message content
            var message = new Message(
                new[] { customer.Email }, // Use the actual customer's email
                $"Your Bookworm Invoice #{savedInvoice.InvoiceId}",
                $"Hi {customer.Name}, thank you for your purchase! Your invoice is attached."
            );

            // Send the email with the PDF attached
            await _emailSender.SendEmailWithAttachmentAsync(message, pdfBytes, $"Invoice-{savedInvoice.InvoiceId}.pdf");
            Console.WriteLine($"✅ Invoice email successfully sent to {customer.Email}");
            // -------------------------------------------

            return MapToOrderResponseDTO(savedInvoice);
        }

        // Helper method to map the Invoice entity to an OrderResponseDTO.
        private OrderResponseDTO MapToOrderResponseDTO(Invoice invoice)
        {
            var response = new OrderResponseDTO
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                OrderDate = invoice.Date,
                TotalAmount = invoice.Amount,
                Status = "COMPLETED"
            };

            // For mapping the nested items, we perform the async lookups here.
            response.Items = invoice.InvoiceDetails.Select(detail =>
            {
                var itemDto = new OrderItemResponseDTO
                {
                    ProductId = detail.Product.Id,
                    ProductName = detail.Product.Name,
                    Price = (double)detail.SellPrice,
                    AcquisitionType = string.Equals(detail.TranType, "SALE", StringComparison.OrdinalIgnoreCase) ? "PURCHASE" : "RENTAL"
                };

                if (string.Equals(itemDto.AcquisitionType, "RENTAL", StringComparison.OrdinalIgnoreCase))
                {
                    // Use a synchronous version here or await the result.
                    // This is an area for potential optimization if performance is critical.
                    var ledger = _rentalLedgerRepository.FindByIdAsync(detail.InvDtlId).Result;
                    itemDto.RentalEndDate = ledger?.RentEndDate;
                }
                return itemDto;
            }).ToList();

            return response;
        }
    }
}
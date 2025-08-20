using Bookworm.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/v1/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("{customerId}")]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetUserInvoices(int customerId)
    {
        if (customerId <= 0)
        {
            return BadRequest("Customer ID must be a positive integer.");
        }

        // You would typically validate that the authenticated user's ID matches the customerId here.
        // For example:
        // if (User.GetUserId() != customerId) { return Forbid(); }

        var orders = await _invoiceService.GetUserOrdersAsync(customerId);
        if (orders == null || !orders.Any())
        {
            return NotFound("No orders found for this customer.");
        }

        return Ok(orders);
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Razorpay.Api;

[Route("api/[controller]")]
[ApiController]
public class RazorpayController : ControllerBase
{
    private readonly RazorpaySettings _settings;

    public RazorpayController(IOptions<RazorpaySettings> settings)
    {
        _settings = settings.Value;
    }

    [HttpPost("create-order")]
    public IActionResult CreateOrder([FromBody] RazorpayOrderRequest request)
    {
        var client = new RazorpayClient(_settings.Key, _settings.Secret);

        var options = new Dictionary<string, object>
        {
            { "amount", request.Amount * 100 }, // paise
            { "currency", "INR" },
            { "payment_capture", 1 }
        };

        Order order = client.Order.Create(options);

        return Ok(new
        {
            key = _settings.Key,
            orderId = order["id"].ToString(),
            amount = order["amount"],
            currency = order["currency"]
        });
    }
}

public class RazorpaySettings
{
    public string Key { get; set; }
    public string Secret { get; set; }
}

public class RazorpayOrderRequest
{
    public int Amount { get; set; }
}

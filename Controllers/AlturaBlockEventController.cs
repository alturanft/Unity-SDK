using Microsoft.AspNetCore.Mvc;
namespace AlturaSDK.Controllers;

[ApiController]
[Route("[controller]")]

public class AlturaBlockEventController : ControllerBase
{
    private readonly ILogger<AlturaBlockEventController> _logger;
    public AlturaBlockEventController(ILogger<AlturaBlockEventController> logger)
    {
        _logger = logger;
    }
    [HttpGet(Name = "GetAlturaBlockEvent")]
    public IEnumerable<AlturaBlockEvent> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new AlturaBlockEvent
        {
            id = "AlturaBlockEvent " + index,
            pairId = "AlturaBlockEvent " + index,
            chainId = "AlturaBlockEvent " + index,
            bidIndex = "AlturaBlockEvent " + index,
            bidTx = "AlturaBlockEvent " + index,
            cancelTx = "AlturaBlockEvent " + index,
            placedAt = DateTime.Now,
            cancelledAt = DateTime.Now,
            from = "AlturaBlockEvent " + index,
            currency = "AlturaBlockEvent " + index,
            price = index,
            priceUSD = index,
            active = true,
            updatedAt = DateTime.Now
        })
        .ToArray();

    } 
}
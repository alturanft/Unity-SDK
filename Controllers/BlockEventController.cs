using Microsoft.AspNetCore.Mvc;
namespace AlturaSDK.Controllers;

[ApiController]
[Route("[controller]")]

public class BlockEventController : ControllerBase
{
    private readonly ILogger<BlockEventController> _logger;
    public BlockEventController(ILogger<BlockEventController> logger)
    {
        _logger = logger;
    }
    [HttpGet(Name = "GetBlockEvent")]
    public IEnumerable<SDK.Assets.Models.BlockEvent> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new SDK.Assets.Models.BlockEvent
        {
            id = "BlockEvent " + index,
            pairId = "BlockEvent " + index,
            chainId = "BlockEvent " + index,
            bidIndex = "BlockEvent " + index,
            bidTx = "BlockEvent " + index,
            cancelTx = "BlockEvent " + index,
            placedAt = DateTime.Now,
            cancelledAt = DateTime.Now,
            from = "BlockEvent " + index,
            currency = "BlockEvent " + index,
            price = index,
            priceUSD = index,
            active = true,
            updatedAt = DateTime.Now
        })
        .ToArray();

    } 
}
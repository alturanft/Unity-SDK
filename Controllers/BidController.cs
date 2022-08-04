using Microsoft.AspNetCore.Mvc;
namespace SDK.Controllers;

[ApiController]
[Route("[controller]")]

public class BidController : ControllerBase
{
    private readonly ILogger<BidController> _logger;
    public BidController(ILogger<BidController> logger)
    {
        _logger = logger;
    }
    [HttpGet(Name = "GetBid")]
    public IEnumerable<SDK.Assets.Models.Bid> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new SDK.Assets.Models.Bid
        {
            id = "Bid " + index,
            chainId = index,
            newEvent = "Bid " + index,
            blockNumber = index,
            timestamp = DateTime.Now,
            to = "Bid " + index,
            from = "Bid " + index,
            transactionHash = "Bid " + index,
            tokenId = index,
            itemCollection = "Bid " + index,
            itemRef = "Bid " + index,
            amount = index,
            price = "Bid " + index,
            currency = "Bid " + index,
            worth = index,
            revenue = index,
            notified = true
  
        })
        .ToArray();
    }
}
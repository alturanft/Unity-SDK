using Microsoft.AspNetCore.Mvc;
namespace AlturaSDK.Controllers;

[ApiController]
[Route("[controller]")]

public class AlturaBidController : ControllerBase
{
    private readonly ILogger<AlturaBidController> _logger;
    public AlturaBidController(ILogger<AlturaBidController> logger)
    {
        _logger = logger;
    }
    [HttpGet(Name = "GetAlturaBid")]
    public IEnumerable<AlturaBid> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new AlturaBid
        {
            id = "AlturaBid " + index,
            chainId = index,
            newEvent = "AlturaBid " + index,
            blockNumber = index,
            timestamp = DateTime.Now,
            to = "AlturaBid " + index,
            from = "AlturaBid " + index,
            transactionHash = "AlturaBid " + index,
            tokenId = index,
            itemCollection = "AlturaBid " + index,
            itemRef = "AlturaBid " + index,
            amount = index,
            price = "AlturaBid " + index,
            currency = "AlturaBid " + index,
            worth = index,
            revenue = index,
            notified = true
  
        })
        .ToArray();
    }
}
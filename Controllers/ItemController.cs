using Microsoft.AspNetCore.Mvc;

namespace AlturaSDK.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{

    private readonly ILogger<ItemController> _logger;

    public ItemController(ILogger<ItemController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetItem")]
    public IEnumerable<SDK.Assets.Models.Item> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new SDK.Assets.Models.Item
        {
            TokenId = "Item " + index,
            ItemCollection = "Item " + index,
            ChainId = "Item " + index,
            ItemRef = "Item " + index,
            Royalty = index,
            CreatorAddress = "Item " + index,
            MintDate = DateTime.Now,
            MintId = "Item " + index,
            Stackable = index % 2 == 0,
            Supply = index,
            MaxSupply = index,
            NonStackableSupply = index,
            Name = "Item " + index,
            Description = "Item " + index,
            ExternalLink = "Item " + index,
            UnlockableContent = "Item " + index,
            Image = "Item " + index,
            ImageHash = "Item " + index,
            ImageUrl = "Item " + index,
            fileType = "Item " + index,
            IsVideo = false,
        })
        .ToArray();
       
    }
}


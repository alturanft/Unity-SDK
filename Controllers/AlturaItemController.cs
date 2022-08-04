using Microsoft.AspNetCore.Mvc;

namespace AlturaSDK.Controllers;

[ApiController]
[Route("[controller]")]
public class AlturaItemController : ControllerBase
{

    private readonly ILogger<AlturaItemController> _logger;

    public AlturaItemController(ILogger<AlturaItemController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAlturaItem")]
    public IEnumerable<AlturaItem> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new AlturaItem
        {
            TokenId = "AlturaItem " + index,
            ItemCollection = "AlturaItem " + index,
            ChainId = "AlturaItem " + index,
            ItemRef = "AlturaItem " + index,
            Royalty = index,
            CreatorAddress = "AlturaItem " + index,
            MintDate = DateTime.Now,
            MintId = "AlturaItem " + index,
            Stackable = index % 2 == 0,
            Supply = index,
            MaxSupply = index,
            NonStackableSupply = index,
            Name = "AlturaItem " + index,
            Description = "AlturaItem " + index,
            ExternalLink = "AlturaItem " + index,
            UnlockableContent = "AlturaItem " + index,
            Image = "AlturaItem " + index,
            ImageHash = "AlturaItem " + index,
            ImageUrl = "AlturaItem " + index,
            FileType = "AlturaItem " + index,
            IsVideo = index % 2 == 0
        })
        .ToArray();
       
    }
}


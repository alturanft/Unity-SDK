using Microsoft.AspNetCore.Mvc;

namespace AlturaSDK.Controllers;

[ApiController]
[Route("[controller]")]
public class AlturaUserController : ControllerBase
{


    private readonly ILogger<AlturaUserController> _logger;

    public AlturaUserController(ILogger<AlturaUserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAlturaUser")]
    public IEnumerable<AlturaUser> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new AlturaUser
        {
            Address = "AlturaUser " + index,
            Name = "AlturaUser " + index,
            Bio = "AlturaUser " + index,
            SocialLink = "AlturaUser " + index,
            ImageHash = "AlturaUser " + index,
            ProfilePic = "AlturaUser " + index,
            ProfilePicUrl = "AlturaUser " + index,
            Nonce = index,
            LastLogin = DateTime.Now,
            AuthCode = "AlturaUser " + index,
            Admin = index % 2 == 0,
            AgreeToTerms = index % 2 == 0,
            Blacklisted = index % 2 == 0
        })
        .ToArray();
    }
}


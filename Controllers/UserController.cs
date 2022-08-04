using Microsoft.AspNetCore.Mvc;

namespace SDK.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{


    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUser")]
    public IEnumerable<SDK.Assets.Models.User> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new SDK.Assets.Models.User
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
            Admin = false,
            AgreeToTerms = true,
            Blacklisted = false,
        }).ToArray();

    }
}


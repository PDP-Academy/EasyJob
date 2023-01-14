using EasyJob.Application.DataTransferObjects;
using EasyJob.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EasyJob.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(
        IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost]
    public async ValueTask<ActionResult<TokenDto>> LoginAsync(
        AuthenticationDto authenticationDto)
    {
        var tokenDto = await this.authenticationService
            .LoginAsync(authenticationDto);

        return Ok(tokenDto);
    }
}

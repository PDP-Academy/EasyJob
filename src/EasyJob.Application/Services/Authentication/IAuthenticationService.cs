using EasyJob.Application.DataTransferObjects;

namespace EasyJob.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<TokenDto> LoginAsync(AuthenticationDto authenticationDto);
    Task<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
}
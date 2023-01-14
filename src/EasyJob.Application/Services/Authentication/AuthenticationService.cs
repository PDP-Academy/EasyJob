using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;
using EasyJob.Infrastructure.Authentication;
using EasyJob.Infrastructure.Repositories.Users;
using System.IdentityModel.Tokens.Jwt;

namespace EasyJob.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenHandler jwtTokenHandler;

    public AuthenticationService(
        IUserRepository userRepository,
        IJwtTokenHandler jwtTokenHandler)
    {
        this.userRepository = userRepository;
        this.jwtTokenHandler = jwtTokenHandler;
    }

    public async Task<TokenDto> LoginAsync(AuthenticationDto authenticationDto)
    {
        var user = await this.userRepository
            .SelectByIdWithDetailsAsync(
                expression: user => user.Email == authenticationDto.userName &&
                    user.PasswordHash == authenticationDto.password,
                includes: Array.Empty<string>());

        var token = this.jwtTokenHandler
            .GenerateJwtToken(user);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken: null,
            expireDate: token.ValidTo);
    }
}
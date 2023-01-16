using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Exceptions;
using EasyJob.Infrastructure.Authentication;
using EasyJob.Infrastructure.Repositories.Users;
using System.IdentityModel.Tokens.Jwt;

namespace EasyJob.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenHandler jwtTokenHandler;
    private readonly IPasswordHasher passwordHasher;

    public AuthenticationService(
        IUserRepository userRepository,
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.jwtTokenHandler = jwtTokenHandler;
        this.passwordHasher = passwordHasher;
    }

    public async Task<TokenDto> LoginAsync(AuthenticationDto authenticationDto)
    {
        var user = await this.userRepository
            .SelectByIdWithDetailsAsync(
                expression: user => user.Email == authenticationDto.email,
                includes: Array.Empty<string>());

        if(user is null)
        {
            throw new NotFoundException("User with given credentials not found");
        }

        if(!this.passwordHasher.Verify(
            hash: user.PasswordHash,
            password: authenticationDto.password,
            salt: user.Salt))
        {
            throw new ValidationException("Username or password is not valid");
        }

        var token = this.jwtTokenHandler
            .GenerateJwtToken(user);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken: null,
            expireDate: token.ValidTo);
    }
}
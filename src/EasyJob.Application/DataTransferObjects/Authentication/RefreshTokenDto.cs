namespace EasyJob.Application.DataTransferObjects;

public record RefreshTokenDto(
    string accessToken,
    string refreshToken);
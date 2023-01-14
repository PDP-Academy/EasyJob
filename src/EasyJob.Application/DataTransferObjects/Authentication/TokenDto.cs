namespace EasyJob.Application.DataTransferObjects;

public record TokenDto(
    string accessToken,
    string? refreshToken,
    DateTime expireDate);
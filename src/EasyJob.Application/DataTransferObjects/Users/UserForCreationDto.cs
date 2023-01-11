namespace EasyJob.Application.DataTransferObjects;

public record UserForCreationDto(
    string firstName,
    string? lastName,
    string phoneNumber,
    string email,
    string password,
    string country,
    string? region,
    string? street,
    short? postalCode);
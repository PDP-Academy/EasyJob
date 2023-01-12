namespace EasyJob.Application.DataTransferObjects;

public record UserForModificationDto(
    Guid userId,
    string? firstName,
    string? lastName,
    string? phoneNumber,
    string? country,
    string? region,
    string? street,
    short? postalCode);
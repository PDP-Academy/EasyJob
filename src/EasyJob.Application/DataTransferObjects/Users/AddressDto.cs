namespace EasyJob.Application.DataTransferObjects;

public record AddressDto(
    string country,
    string? region,
    string? street,
    short? postCode);

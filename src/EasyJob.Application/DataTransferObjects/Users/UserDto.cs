using EasyJob.Domain.Entities.Users;
using EasyJob.Domain.Enums;

namespace EasyJob.Application.DataTransferObjects;

public record UserDto(
    Guid id,
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    UserRole role,
    AddressDto? address);
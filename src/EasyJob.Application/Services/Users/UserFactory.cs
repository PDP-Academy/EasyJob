using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;
using EasyJob.Domain.Enums;
using EasyJob.Infrastructure.Authentication;

namespace EasyJob.Application.Services.Users;

public class UserFactory : IUserFactory
{
    private readonly IPasswordHasher passwordHasher;

    public UserFactory(IPasswordHasher passwordHasher)
    {
        this.passwordHasher = passwordHasher;
    }

    public User MapToUser(
        UserForCreationDto userForCreationDto)
    {
        string randomSalt = Guid.NewGuid().ToString();

        return new User
        {
            FirstName = userForCreationDto.firstName,
            LastName = userForCreationDto.lastName,
            Email = userForCreationDto.email,
            PhoneNumber = userForCreationDto.phoneNumber,
            
            Address = new Address
            {
                Street = userForCreationDto.street,
                Country = userForCreationDto.country,
                Region = userForCreationDto.region,
                PostalCode = userForCreationDto.postalCode,
            },
            
            Salt = randomSalt,

            PasswordHash = this.passwordHasher.Encrypt(
                password: userForCreationDto.password,
                salt: randomSalt),
            
            Role = UserRole.Candidate
        };
    }

    public void MapToUser(
        User storageUser,
        UserForModificationDto userForModificationDto)
    {
        storageUser.FirstName = userForModificationDto.firstName ?? storageUser.FirstName;
        storageUser.LastName = userForModificationDto.lastName;
        storageUser.PhoneNumber = userForModificationDto.phoneNumber ?? storageUser.PhoneNumber;
        storageUser.UpdatedAt = DateTime.UtcNow;

        storageUser.Address = storageUser.Address ?? new Address();
        storageUser.Address.Country = userForModificationDto.country ?? storageUser.Address.Country;
        storageUser.Address.Region = userForModificationDto.region;
        storageUser.Address.Street = userForModificationDto.street;
        storageUser.Address.PostalCode = userForModificationDto.postalCode;
    }

    public UserDto MapToUserDto(User user)
    {
        AddressDto? addressDto = default;

        if (user.Address is not null)
        {
            addressDto = new AddressDto(
                user.Address.Country,
                user.Address.Region,
                user.Address.Street,
                user.Address.PostalCode);
        }

        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName!,
            user.Email,
            user.PhoneNumber,
            user.Role,
            addressDto);
    }
}
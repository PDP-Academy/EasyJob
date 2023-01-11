using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;

namespace EasyJob.Application.Services.Users;

public class UserFactory : IUserFactory
{
    public User MapToUser(
        UserForCreationDto userForCreationDto)
    {
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
            }
        };
    }

    public UserDto MapToUserDto(User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName!,
            user.Email,
            user.PhoneNumber,
            user.Role);
    }
}
using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;

namespace EasyJob.Application.Services.Users;

public interface IUserFactory
{
    UserDto MapToUserDto(User user);
    User MapToUser(UserForCreationDto userForCreationDto);
}
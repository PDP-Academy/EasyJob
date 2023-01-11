using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;

namespace EasyJob.Application.Services.Users;

public interface IUserService
{
    ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto);
    IQueryable<UserDto> RetrieveUsers();
    ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId);
    ValueTask<UserDto> ModifyUserAsync(User user);
    ValueTask<UserDto> RemoveUserAsync(Guid user);
}
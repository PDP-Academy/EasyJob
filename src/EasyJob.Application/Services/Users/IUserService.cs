using EasyJob.Application.DataTransferObjects;

namespace EasyJob.Application.Services.Users;

public interface IUserService
{
    ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto);
    IQueryable<UserDto> RetrieveUsers();
    ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId);
    ValueTask<UserDto> ModifyUserAsync(UserForModificationDto userForModificationDto);
    ValueTask<UserDto> RemoveUserAsync(Guid user);
}
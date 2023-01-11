using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;
using EasyJob.Infrastructure.Repositories.Users;

namespace EasyJob.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserFactory userFactory;

    public UserService(
        IUserRepository userRepository,
        IUserFactory userFactory)
    {
        this.userRepository = userRepository;
        this.userFactory = userFactory;
    }

    public async ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto)
    {
        var newUser = this.userFactory.MapToUser(userForCreationDto);

        var addedUser = await this.userRepository
            .InsertAsync(newUser);

        return this.userFactory.MapToUserDto(addedUser);
    }

    public IQueryable<UserDto> RetrieveUsers()
    {
        var users = this.userRepository.SelectAll();

        return users.Select(user =>
            this.userFactory.MapToUserDto(user));
    }

    public async ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId)
    {
        var storageUser = await this.userRepository
            .SelectByIdAsync(userId);

        return this.userFactory.MapToUserDto(storageUser);
    }

    public async ValueTask<UserDto> ModifyUserAsync(User user)
    {
        var modifiedUser = await this.userRepository
            .UpdateAsync(user);

        return this.userFactory.MapToUserDto(modifiedUser);
    }

    public async ValueTask<UserDto> RemoveUserAsync(Guid userId)
    {
        var user = await this.userRepository
            .SelectByIdAsync(userId);

        var removedUser = await this.userRepository
            .DeleteAsync(user);

        return this.userFactory.MapToUserDto(removedUser);
    }
}
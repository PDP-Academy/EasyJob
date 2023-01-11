using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;
using EasyJob.Infrastructure.Repositories.Users;

namespace EasyJob.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserFactory userFactory;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async ValueTask<User> CreateUserAsync(User user)
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

    public async ValueTask<User> RetrieveUserByIdAsync(Guid userId)
    {
        return await this.userRepository
            .SelectByIdAsync(userId);
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
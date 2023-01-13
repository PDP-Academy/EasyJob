using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;
using EasyJob.Infrastructure.Repositories.Users;

namespace EasyJob.Application.Services.Users;

public partial class UserService : IUserService
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

    public async ValueTask<UserDto> CreateUserAsync(
        UserForCreationDto userForCreationDto)
    {
        ValidateUserForCreationDto(userForCreationDto);

        var newUser = this.userFactory
            .MapToUser(userForCreationDto);

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
        ValidateUserId(userId);

        var storageUser = await this.userRepository
            .SelectByIdWithDetailsAsync(
                expression: user =>
                    user.Id == userId,
                includes: new string[] { nameof(User.Address) });

        ValidateStorageUser(storageUser, userId);

        return this.userFactory.MapToUserDto(storageUser);
    }

    public async ValueTask<UserDto> ModifyUserAsync(
        UserForModificationDto userForModificationDto)
    {
        ValidateUserForModificationnDto(userForModificationDto);

        var storageUser = await this.userRepository
            .SelectByIdWithDetailsAsync(
                expression: user =>
                    user.Id == userForModificationDto.userId,
                includes: new string[] { nameof(User.Address) });

        ValidateStorageUser(storageUser, userForModificationDto.userId);

        this.userFactory.MapToUser(storageUser, userForModificationDto);

        var modifiedUser = await this.userRepository
            .UpdateAsync(storageUser);

        return this.userFactory.MapToUserDto(modifiedUser);
    }

    public async ValueTask<UserDto> RemoveUserAsync(Guid userId)
    {
        ValidateUserId(userId);

        var storageUser = await this.userRepository
            .SelectByIdAsync(userId);

        ValidateStorageUser(storageUser, userId);

        var removedUser = await this.userRepository
            .DeleteAsync(storageUser);

        return this.userFactory.MapToUserDto(removedUser);
    }
}
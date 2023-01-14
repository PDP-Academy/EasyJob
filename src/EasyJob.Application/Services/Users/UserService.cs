using EasyJob.Application.DataTransferObjects;
using EasyJob.Application.Extensions;
using EasyJob.Application.Models;
using EasyJob.Domain.Entities.Users;
using EasyJob.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Http;

namespace EasyJob.Application.Services.Users;

public partial class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserFactory userFactory;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(
        IUserRepository userRepository,
        IUserFactory userFactory,
        IHttpContextAccessor httpContextAccessor)
    {
        this.userRepository = userRepository;
        this.userFactory = userFactory;
        this.httpContextAccessor = httpContextAccessor;
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

    public IQueryable<UserDto> RetrieveUsers(
        QueryParameter queryParameter)
    {
        var users = this.userRepository
            .SelectAll()
            .ToPagedList(
                httpContext: this.httpContextAccessor.HttpContext,
                pageSize: queryParameter.Page.Size,
                pageIndex: queryParameter.Page.Index);

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
        ValidateUserForModificationDto(userForModificationDto);

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
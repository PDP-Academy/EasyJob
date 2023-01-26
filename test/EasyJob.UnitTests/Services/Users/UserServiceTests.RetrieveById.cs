using EasyJob.Application.DataTransferObjects;
using EasyJob.Domain.Entities.Users;
using EasyJob.Domain.Enums;
using EasyJob.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace EasyJob.UnitTests.Services.Users;

public partial class UserServiceTests
{
    [Fact]
    public async Task Should_ThrowValidationExceptionOnInvalidId()
    {
        var randomUserId = default(Guid);
        var exceptionMessage = $"The given userId is invalid: {randomUserId}";
        
        var validationException =
            new ValidationException(exceptionMessage);

        ValueTask<UserDto> userDtoTask = this.userService
            .RetrieveUserByIdAsync(randomUserId);

        var validationExceptionResult = await Assert
            .ThrowsAsync<ValidationException>(userDtoTask.AsTask);

        Assert.Equal(validationException.Message, validationExceptionResult.Message);
    }

    [Fact]
    public async Task Should_ThrowNotFoundExceptionOnRetrieveById()
    {
        var randomUserId = Guid.NewGuid();
        User storageUser = null;

        userMockRepository.Setup(mock => mock.SelectByIdWithDetailsAsync(
                user => user.Id == randomUserId,
                new string[] { nameof(User.Address) }))
            .ReturnsAsync(storageUser);

        ValueTask<UserDto> userDtoTask = this.userService
            .RetrieveUserByIdAsync(randomUserId);

        await Assert.ThrowsAsync<NotFoundException>(userDtoTask.AsTask);
    }

    [Fact]
    public async Task Should_RetrieveByIdAsync()
    {
        var randomUserId = Guid.NewGuid();

        User storageUser = new User
        {
            Id = randomUserId,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            PhoneNumber = "Test",
            Role = UserRole.Admin
        };

        UserDto userDto = new UserDto(
            Guid.NewGuid(),
            storageUser.LastName,
            storageUser.FirstName,
            storageUser.Email,
            storageUser.PhoneNumber,
            storageUser.Role,
            null);

        userMockRepository.Setup(mock => mock.SelectByIdWithDetailsAsync(
                user => user.Id == randomUserId,
                new string[] { nameof(User.Address) }))
            .ReturnsAsync(storageUser);

        userMockFactory.Setup(mock => mock
                .MapToUserDto(storageUser))
            .Returns(userDto);

        var actualUser = await this.userService
            .RetrieveUserByIdAsync(randomUserId);

        Assert.Equal(randomUserId, actualUser.id);
    }
}
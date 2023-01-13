using EasyJob.Application.DataTransferObjects;
using EasyJob.Application.Validator;
using EasyJob.Application.Validators.Users;
using EasyJob.Domain.Entities.Users;
using EasyJob.Domain.Exceptions;
using FluentValidation.Results;
using System.Text.Json;

namespace EasyJob.Application.Services.Users;

public partial class UserService
{
    public void ValidateUserId(Guid userId)
    {
        if(userId == default)
        {
            throw new ValidationException($"The given userId is invalid: {userId}");
        }
    }

    public void ValidateStorageUser(User storageUser, Guid userId)
    {
        if(storageUser is null)
        {
            throw new NotFoundException($"Couldn't find user with given id: {userId}");
        }
    }

    public void ValidateUserForCreationDto(
        UserForCreationDto userForCreationDto)
    {
        var validationResult = new UserForCreationDtoValidator()
            .Validate(userForCreationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    public void ValidateUserForModificationnDto(
        UserForModificationDto userForModificationDto)
    {
        var validationResult = new UserForModificationDtoValidator()
            .Validate(userForModificationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    private static void ThrowValidationExceptionIfValidationIsInvalid(ValidationResult validationResult)
    {
        if (validationResult.IsValid)
        {
            return;
        }

        var errors = JsonSerializer
                .Serialize(validationResult.Errors.Select(error => new
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                    AttemptedValue = error.AttemptedValue
                }));

        throw new ValidationException(errors);
    }
}
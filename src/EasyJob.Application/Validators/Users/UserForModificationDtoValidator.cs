using EasyJob.Application.DataTransferObjects;
using FluentValidation;

namespace EasyJob.Application.Validators.Users;

public class UserForModificationDtoValidator : AbstractValidator<UserForModificationDto>
{
	public UserForModificationDtoValidator()
	{
        RuleFor(user => user)
            .NotNull();

        RuleFor(user => user.userId)
            .NotEqual(default(Guid));
    }
}
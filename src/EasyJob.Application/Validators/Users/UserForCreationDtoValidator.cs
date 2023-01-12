using EasyJob.Application.DataTransferObjects;
using FluentValidation;

namespace EasyJob.Application.Validator;

public class UserForCreationDtoValidator : AbstractValidator<UserForCreationDto>
{
	public UserForCreationDtoValidator()
	{
		RuleFor(user => user)
			.NotNull();

		RuleFor(user => user.firstName)
			.MaximumLength(100)
			.NotEmpty();

		RuleFor(user => user.email)
			.MaximumLength(100)
			.EmailAddress().WithMessage("Email noto'g'ri")
            .NotEmpty();
    }
}
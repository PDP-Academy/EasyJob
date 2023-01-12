using EasyJob.Domain.Entities.Users;
using FluentValidation;

namespace EasyJob.Application.Validators.Users;

public class UserValidator : AbstractValidator<User>
{
	public UserValidator()
	{
		RuleFor(user => user)
			.NotEmpty();
	}
}
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Users.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    /// <summary>
    /// Validates the data for user update
    /// </summary>
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY)
            .MaximumLength(100)
            .WithMessage(ResourceErrorMessages.NAME_TOO_LONG)
            .WithName(nameof(User.Name));
        RuleFor(user => user.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID)
            .MaximumLength(100)
            .WithMessage(ResourceErrorMessages.EMAIL_TOO_LONG)
            .WithName(nameof(User.Email));
        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator<RequestUpdateUserJson>());
    }
}
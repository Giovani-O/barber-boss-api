using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.BarberShops.Register;

/// <summary>
/// Validates the data for barber shop registration
/// </summary>
public class RegisterBarberShopValidator : AbstractValidator<RequestRegisterBarberShopJson>
{
    public RegisterBarberShopValidator()
    {
        RuleFor(barberShop => barberShop.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY)
            .MaximumLength(100)
            .WithMessage(ResourceErrorMessages.NAME_TOO_LONG)
            .WithName(nameof(BarberShop.Name));

        RuleFor(barberShop => barberShop.UserId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.ID_IS_EMPTY)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.ID_IS_INVALID)
            .WithName(nameof(BarberShop.UserId));
    }
}
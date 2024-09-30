using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.BarberShops.Update;

public class UpdateBarberShopValidator : AbstractValidator<RequestUpdateBarberShopJson>
{
    public UpdateBarberShopValidator()
    {
        RuleFor(b => b.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY)
            .MaximumLength(100)
            .WithMessage(ResourceErrorMessages.NAME_TOO_LONG)
            .WithName(nameof(BarberShop.Name));
    }
}
using FluentValidation;
using OrderService.Application.Features.Orders.Commands;


namespace OrderService.Application.Features.Orders.Handlers;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.DeliveryAddress).NotEmpty().MaximumLength(200);
    }
}

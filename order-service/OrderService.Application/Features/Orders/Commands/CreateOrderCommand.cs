using MediatR;


namespace OrderService.Application.Features.Orders.Commands;


public record CreateOrderCommand(string CustomerId, string DeliveryAddress) : IRequest<Guid>;


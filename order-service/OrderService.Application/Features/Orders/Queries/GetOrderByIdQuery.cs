using MediatR;
using OrderService.Domain;

namespace OrderService.Application.Features.Orders.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<Order>;


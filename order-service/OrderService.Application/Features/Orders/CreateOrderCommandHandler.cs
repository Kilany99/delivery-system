
using MediatR;
using OrderService.Application.Features.Orders.Commands;
using OrderService.Domain;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Application.Features.Orders;


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repo;

    public CreateOrderCommandHandler(IOrderRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerId, request.DeliveryAddress);
        await _repo.AddAsync(order);
        return order.Id;
    }
}
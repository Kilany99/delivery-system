
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Features.Orders.Commands;
using OrderService.Domain;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Application.Features.Orders.Handlers;


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repo;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    
    public CreateOrderCommandHandler(IOrderRepository repo, ILogger<CreateOrderCommandHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating order for customer {CustomerId}", request.CustomerId);
        var order = Order.Create(request.CustomerId, request.DeliveryAddress);
        await _repo.AddAsync(order);
        return order.Id;
    }
}
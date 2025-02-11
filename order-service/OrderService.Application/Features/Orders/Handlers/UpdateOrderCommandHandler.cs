using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Features.Orders.Commands;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Application.Features.Orders.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand,Guid>
{
    private readonly IOrderRepository _repo;
    private readonly ILogger<UpdateOrderCommand> _logger;

    public UpdateOrderCommandHandler(IOrderRepository repo, ILogger<UpdateOrderCommand> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating order {OrderId}", request.OrderId);
        var order = await _repo.GetByIdAsync(request.OrderId)??
            throw new OrderDomainException("Order not found");

        if (request.CustomerId != null)
            order.UpdateCustomerId(request.CustomerId);

        if (request.DeliveryAddress != null)
            order.UpdateDeliveryAddress(request.DeliveryAddress);
        order.UpdateStatus(request.Status);

        await _repo.UpdateAsync(order);
        return order.Id;
    }
}
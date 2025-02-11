using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Orders.Commands;
using OrderService.Application.Features.Orders.Queries;

namespace OrderService.API.Controllers;
/// <summary>
/// Service endpoint for managing orders.
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator) => _mediator = mediator;
   
    
    /// <summary>Creates a new order.</summary>
    /// <example>{"customerId": "cust-123", "deliveryAddress": "123 Main St"}</example>
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
    }

    /// <summary>Gets an order with its guide.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(order);
    }
    /// <summary>Updates order status.</summary>

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusCommand command)
    {
        if (id != command.OrderId)
            return BadRequest("Order ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }
    /// <summary>Updates an order.</summary>

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
    {
        if (id != command.OrderId)
            return BadRequest("ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }
}
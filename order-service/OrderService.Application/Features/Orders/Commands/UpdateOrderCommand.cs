﻿using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain;


namespace OrderService.Application.Features.Orders.Commands;

public record UpdateOrderCommand(
    Guid OrderId,
    string? CustomerId,
    string? DeliveryAddress,
    OrderStatus Status
) : IRequest<ApiResponse<Guid>>;
using OrderService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Responses;

public record OrderResponse(Guid Id, string CustomerId, string DeliveryAddress, OrderStatus Status);


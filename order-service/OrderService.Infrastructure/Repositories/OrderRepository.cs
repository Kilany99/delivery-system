using OrderService.Domain;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories;


public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order> GetByIdAsync(Guid id);
}
public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context) => _context = context;

    public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);
    public async Task<Order> GetByIdAsync(Guid id)=> await _context.Orders.FindAsync(id)??
            throw new NullReferenceException("Order not found for provided ID");
        
    
}
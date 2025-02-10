namespace OrderService.Domain;
public class Order : Entity<Guid>
{
    public string CustomerId { get; private set; }
    public string DeliveryAddress { get; private set; }
    public OrderStatus Status { get; private set; }

    // Factory method for controlled creation
    public static Order Create(string customerId, string deliveryAddress)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            DeliveryAddress = deliveryAddress,
            Status = OrderStatus.Created
        };
    }
    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }
    public void MarkAsOutForDelivery() => Status = OrderStatus.OutForDelivery;
}

public enum OrderStatus { Created, Preparing, OutForDelivery, Delivered }
using Confluent.Kafka;
using OrderService.API.Models;
using OrderService.API.Serialization;
using OrderService.Infrastructure.Repositories;
using System.Text.Json;

namespace OrderService.API.Consumers;

public class DriverLocationConsumer : IHostedService
{
    private readonly IConsumer<string, DriverLocationEvent> _consumer;
    private readonly ILogger<DriverLocationConsumer> _logger;
    private readonly IOrderRepository _orderRepository;

    public DriverLocationConsumer(ILogger<DriverLocationConsumer> logger,IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository; 

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "order-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, DriverLocationEvent>(config)
            .SetValueDeserializer(new JsonDeserializer<DriverLocationEvent>())
            .Build();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe("driver-location-updates");

        Task.Run(async () => 
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);
                    var location = result.Message.Value;

                    _logger.LogInformation(
                        "Received driver location: DriverId={DriverId}, Lat={Lat}, Lng={Lng}",
                        location.DriverId,
                        location.Latitude,
                        location.Longitude
                    );

                    // Add logic to check proximity to orders
                    var orders = await _orderRepository.GetOrdersNearLocationAsync(
                        location.Latitude,
                        location.Longitude,
                        1000 // 1km radius
                    );

                    foreach (var order in orders)
                    {
                        order.MarkAsOutForDelivery();
                        await _orderRepository.UpdateAsync(order);
                        _logger.LogInformation("Order {OrderId} is out for delivery", order.Id);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming Kafka message");
                }
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return Task.CompletedTask;
    }
}
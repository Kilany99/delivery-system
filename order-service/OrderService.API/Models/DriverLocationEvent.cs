namespace OrderService.API.Models;



public class DriverLocationEvent
{
    public string DriverId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; }
}
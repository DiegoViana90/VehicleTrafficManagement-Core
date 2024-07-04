namespace VehicleTrafficManagement.Models;

public class TrafficLog
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public DateTime ReturnDateTime { get; set; }
    public int DriverId { get; set; }
    public Driver Driver { get; set; }
    public int Mileage { get; set; }
}

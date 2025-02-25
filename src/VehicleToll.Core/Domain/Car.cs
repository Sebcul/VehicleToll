using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.Core.Domain;

public class Car : Vehicle
{
    public Car() : base(VehicleTypeConstants.Car) { }
}
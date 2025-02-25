using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.UnitTests.Common;

public class FakeVehicle : Vehicle
{
    public FakeVehicle(string vehicleType)
    {
        Type = vehicleType;
    }
}
using VehicleToll.Core.Domain;

namespace VehicleToll.UnitTests.Common;

public class FakeVehicle : Vehicle
{
    public FakeVehicle(string vehicleType) : base(vehicleType) { }
}
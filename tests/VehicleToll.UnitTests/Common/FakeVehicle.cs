using VehicleToll.Core.Domain.Interfaces;

namespace VehicleToll.UnitTests.Common;

public class FakeVehicle : IVehicle
{
    private readonly string _vehicleType;

    public FakeVehicle(string vehicleType)
    {
        _vehicleType = vehicleType;
    }

    public string GetVehicleType() => _vehicleType;
}
using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.Core.Domain.Abstractions;

public abstract class Vehicle : IVehicle
{
    private readonly string[] _tollFreeVehicles =
    [
        VehicleTypeConstants.Motorbike,
        VehicleTypeConstants.Tractor,
        VehicleTypeConstants.Diplomat,
        VehicleTypeConstants.Emergency,
        VehicleTypeConstants.Military,
        VehicleTypeConstants.Foreign
    ];

    protected Vehicle(string type)
    {
        Type = type;
    }

    public string Type { get; }

    public string GetVehicleType() => Type;

    public bool IsTollFree() => _tollFreeVehicles.Contains(Type);
}
using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.Core.Domain.Abstractions;

public abstract class Vehicle : IVehicle
{
    protected readonly string[] TollFreeVehicles =
    [
        VehicleTypeConstants.Motorbike,
        VehicleTypeConstants.Tractor,
        VehicleTypeConstants.Diplomat,
        VehicleTypeConstants.Emergency,
        VehicleTypeConstants.Military,
        VehicleTypeConstants.Foreign
    ];

    public string Type { get; set; }
    
    public string GetVehicleType() => Type;

    public bool IsTollFree() => TollFreeVehicles.Contains(Type);
}
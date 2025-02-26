namespace VehicleToll.Core.Domain;

public abstract class Vehicle
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

    public bool IsTollFree() => _tollFreeVehicles.Contains(Type);
}
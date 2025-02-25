namespace VehicleToll.Core.Domain.Abstractions;

public interface IVehicle
{
    string GetVehicleType();
    public bool IsTollFree();
}
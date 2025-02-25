using VehicleToll.Core.Domain.Interfaces;

namespace VehicleToll.Core.Domain;

public class Motorbike : IVehicle
{
    public string GetVehicleType()
    {
        return VehicleTypeConstants.Motorbike;
    }
}
using System.Collections.Generic;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.UnitTests.Common;

public static class Data
{
    public static IEnumerable<object[]> TollFreeVehicles =>
        new List<IVehicle[]>
        {
            new IVehicle[] { new Motorbike() },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Diplomat) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Emergency) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Foreign) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Military) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Tractor) }
        };
}
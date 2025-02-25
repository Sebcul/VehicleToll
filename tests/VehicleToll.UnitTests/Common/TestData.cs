using System;
using System.Collections.Generic;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.UnitTests.Common;

public static class InlineData
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


public static class Data
{
    public static IEnumerable<DateTime> Holidays2025 =>
        new List<DateTime>
        {
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 6),
            new DateTime(2025, 4, 18),
            new DateTime(2025, 4, 20),
            new DateTime(2025, 5, 1),
            new DateTime(2025, 5, 29),
            new DateTime(2025, 6, 6),
            new DateTime(2025, 6, 8),
            new DateTime(2025, 6, 21),
            new DateTime(2025, 11, 1),
            new DateTime(2025, 12, 25),
            new DateTime(2025, 12, 26)
        };
}
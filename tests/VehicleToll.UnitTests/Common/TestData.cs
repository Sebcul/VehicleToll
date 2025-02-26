using System;
using System.Collections.Generic;
using VehicleToll.Core.Domain;

namespace VehicleToll.UnitTests.Common;

public static class InlineData
{
    public static IEnumerable<object[]> TollFreeVehicles =>
        new List<Vehicle[]>
        {
            new Vehicle[] { new Motorbike() },
            new Vehicle[] { new FakeVehicle(VehicleTypeConstants.Diplomat) },
            new Vehicle[] { new FakeVehicle(VehicleTypeConstants.Emergency) },
            new Vehicle[] { new FakeVehicle(VehicleTypeConstants.Foreign) },
            new Vehicle[] { new FakeVehicle(VehicleTypeConstants.Military) },
            new Vehicle[] { new FakeVehicle(VehicleTypeConstants.Tractor) }
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
﻿using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.Core.Domain;

public class Motorbike : Vehicle
{
    public Motorbike()
    {
        Type = VehicleTypeConstants.Motorbike;
    }
}
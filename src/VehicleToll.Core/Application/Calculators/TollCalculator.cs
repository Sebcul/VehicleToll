﻿using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Interfaces;

namespace VehicleToll.Core.Application.Calculators;

public class TollCalculator
{
    /**
* Calculate the total toll fee for one day
*
* @param vehicle - the vehicle
* @param dates   - date and time of all passes on one day
* @return - the total toll fee for that day
*/
    private readonly string[] _tollFreeVehicles =
    [
        VehicleTypeConstants.Motorbike,
        VehicleTypeConstants.Tractor,
        VehicleTypeConstants.Diplomat,
        VehicleTypeConstants.Emergency,
        VehicleTypeConstants.Military,
        VehicleTypeConstants.Foreign
    ];

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        var intervalStart = dates[0];
        var totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
        {
            return 0;
        }

        var dateRangeFee = DateRangeFee.TollFeeRanges.FirstOrDefault(x => x.Contains(date.TimeOfDay));
        return dateRangeFee?.Fee ?? 0;
    }


    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null)
        {
            return false;
        }

        return _tollFreeVehicles.Contains(vehicle.GetVehicleType());
    }

    private bool IsTollFreeDate(DateTime date)
    {
        var year = date.Year;
        var month = date.Month;
        var day = date.Day;

        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || date.Month == (int)Months.July)
        {
            return true;
        }

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
}
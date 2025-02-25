using System.Diagnostics.CodeAnalysis;
using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.Core.Application.Calculators;

public class TollCalculator
{
    private const int TollIntervalMinutes = 60;
    private const int DailyMaxFee = 60;

    private readonly ITollFreeDatesService _tollFreeDatesService;

    public TollCalculator(ITollFreeDatesService tollFreeDatesService)
    {
        _tollFreeDatesService = tollFreeDatesService;
    }

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        if (dates.Length == 0)
        {
            return 0;
        }

        Array.Sort(dates);

        int dailyFee = 0;
        DateTime? intervalStart = null;
        int currentMaxFeeInInterval = 0;

        foreach (var date in dates)
        {
            int currentFee = GetTollFee(vehicle, date);
            if (currentFee == 0)
            {
                continue;
            }

            if (intervalStart == null)
            {
                intervalStart = date;
                currentMaxFeeInInterval = currentFee;
            }
            else if (IsIn60MinutesInterval(intervalStart.Value, date))
            {
                if (currentFee > currentMaxFeeInInterval)
                {
                    currentMaxFeeInInterval = currentFee;
                }
            }
            else
            {
                dailyFee += currentMaxFeeInInterval;

                intervalStart = date;
                currentMaxFeeInInterval = currentFee;
            }
        }

        if (intervalStart != null)
        {
            dailyFee += currentMaxFeeInInterval;
        }

        return dailyFee > DailyMaxFee ? DailyMaxFee : dailyFee;
    }

    public int GetTollFee(IVehicle vehicle, DateTime date)
    {
        if (_tollFreeDatesService.IsTollFreeDate(date) || vehicle.IsTollFree())
        {
            return 0;
        }

        var dateRangeFee = DateRangeFee.TollFeeRanges.FirstOrDefault(x => x.Contains(date.TimeOfDay));
        return dateRangeFee?.Fee ?? 0;
    }
    
    private static bool IsIn60MinutesInterval(DateTime intervalStart, DateTime date)
    {
        return (date - intervalStart).TotalMinutes <= TollIntervalMinutes;
    }
}
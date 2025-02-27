using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Domain;

namespace VehicleToll.Core.Application.Calculators;

public class TollCalculator
{
    private const int TollIntervalMinutes = 60;

    private readonly ITollFreeDatesService _tollFreeDatesService;

    public TollCalculator(ITollFreeDatesService tollFreeDatesService)
    {
        _tollFreeDatesService = tollFreeDatesService;
    }

public int GetTollFee(Vehicle vehicle, DateTime[] dates)
{
    if (dates.Length == 0)
    {
        return 0;
    }

    var dailyCalculation = new DailyTollCalculation();
    
    foreach (var date in dates.OrderBy(d => d))
    {
        int currentFee = GetTollFee(vehicle, date);
        dailyCalculation.ProcessFee(date, currentFee);
    }

    return dailyCalculation.CalculateFinalFee();
}

    public int GetTollFee(Vehicle vehicle, DateTime date)
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
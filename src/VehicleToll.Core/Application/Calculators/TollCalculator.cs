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
        var intervalStart = dates[0];
        var totalFee = 0;

        foreach (var date in dates)
        {
            int nextFee = GetTollFee(vehicle, date);
            int tempFee = GetTollFee(vehicle, intervalStart);

            var minutesDifferenceBetweenPasses = (date - intervalStart).TotalMinutes;

            if (minutesDifferenceBetweenPasses <= TollIntervalMinutes)
            {
                if (totalFee > 0)
                {
                    totalFee -= tempFee;
                }

                if (nextFee >= tempFee)
                {
                    tempFee = nextFee;
                }
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }

        if (totalFee > DailyMaxFee)
        {
            totalFee = DailyMaxFee;
        }

        return totalFee;
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
}
using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Application.Dates.Holidays;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Abstractions;

namespace VehicleToll.Core.Application.Calculators;

public class TollCalculator
{
    private readonly IHolidayService _holidayService;

    //TODO: Implement holidayService instead of checking dates manually in IsTollFreeDate(DateTime date) method
    public TollCalculator(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }
    
    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        var intervalStart = dates[0];
        var totalFee = 0;
        foreach (var date in dates)
        {
            int nextFee = GetTollFee(vehicle, date);
            int tempFee = GetTollFee(vehicle, intervalStart);
            
            var diff = date - intervalStart;
            var minutes = diff.TotalMinutes;

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

    public int GetTollFee(IVehicle vehicle, DateTime date)
    {
        if (IsTollFreeDate(date) || vehicle.IsTollFree())
        {
            return 0;
        }

        var dateRangeFee = DateRangeFee.TollFeeRanges.FirstOrDefault(x => x.Contains(date.TimeOfDay));
        return dateRangeFee?.Fee ?? 0;
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
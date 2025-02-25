using VehicleToll.Core.Application.Dates.Holidays;

namespace VehicleToll.Core.Application.Dates;

public class TollFreeDatesService : ITollFreeDatesService

{
    private readonly IHolidayService _holidayService;

    public TollFreeDatesService(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    public bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || date.Month == (int)Months.July)
        {
            return true;
        }

        var holidays = _holidayService.GetHolidaysForYear(date.Year).Select(x => x.Date.Date).ToHashSet();
        return holidays.Contains(date.Date) || holidays.Contains(date.Date.AddDays(1));
    }
}
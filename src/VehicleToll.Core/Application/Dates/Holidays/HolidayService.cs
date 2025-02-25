using System.Globalization;

namespace VehicleToll.Core.Application.Dates.Holidays;

public class HolidayService : IHolidayService
{
    private readonly List<YearHolidays> _holidayData;

    public HolidayService(IHolidayFileParser fileParser)
    {
        _holidayData = fileParser.ReadData(Directory.GetCurrentDirectory());
    }

    public List<DateTime> GetHolidaysForYear(int year)
    {
        var yearHolidays = _holidayData.FirstOrDefault(y => y.year == year);
        if (yearHolidays != null)
        {
            return yearHolidays.dates
                    .Select(h => DateTime.ParseExact(h.date, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ToList();
        }
        
        return [];
    }
}
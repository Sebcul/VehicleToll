using System.Globalization;
using VehicleToll.Core.Application.Dates.Holidays.DTOs;

namespace VehicleToll.Core.Application.Dates.Holidays;

public class HolidayService : IHolidayService
{
    private readonly List<YearHolidaysDTO> _holidayData;

    public HolidayService(IHolidayFileParser fileParser)
    {
        _holidayData = fileParser.ReadData(Directory.GetCurrentDirectory());
    }

    public List<DateTime> GetHolidaysForYear(int year)
    {
        var yearHolidays = _holidayData.FirstOrDefault(y => y.Year == year);
        if (yearHolidays != null)
        {
            return yearHolidays.Dates
                    .Select(h => DateTime.ParseExact(h.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ToList();
        }
        
        return [];
    }
}
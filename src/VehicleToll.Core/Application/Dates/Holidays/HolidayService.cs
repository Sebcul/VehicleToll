using System.Globalization;
using VehicleToll.Core.Application.Dates.Holidays.DTOs;

namespace VehicleToll.Core.Application.Dates.Holidays;

public class HolidayService : IHolidayService
{
    private readonly Dictionary<int, List<HolidayDTO>> _holidayData;

    public HolidayService(IHolidayFileParser fileParser)
    {
        _holidayData = fileParser.ReadData(Directory.GetCurrentDirectory()).ToDictionary(ks => ks.Year, es => es.Dates);
    }

    public List<DateTime> GetHolidaysForYear(int year)
    {
        var yearHolidays = _holidayData.GetValueOrDefault(year);
        if (yearHolidays != null)
        {
            return yearHolidays
                    .Select(h => DateTime.ParseExact(h.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ToList();
        }
        
        return [];
    }
}
using VehicleToll.Core.Application.Dates.Holidays.DTOs;

namespace VehicleToll.Core.Application.Dates.Holidays;

public interface IHolidayFileParser
{
    public List<YearHolidaysDTO> ReadData(string directory);
}
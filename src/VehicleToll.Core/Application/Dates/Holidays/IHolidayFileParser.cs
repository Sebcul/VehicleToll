namespace VehicleToll.Core.Application.Dates.Holidays;

public interface IHolidayFileParser
{
    public List<YearHolidays> ReadData(string directory);
}
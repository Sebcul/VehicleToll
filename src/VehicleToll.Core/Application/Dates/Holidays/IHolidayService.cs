namespace VehicleToll.Core.Application.Dates.Holidays;

public interface IHolidayService
{
    public List<DateTime> GetHolidaysForYear(int year);
}
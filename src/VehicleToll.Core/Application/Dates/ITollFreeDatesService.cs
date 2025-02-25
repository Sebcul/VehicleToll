namespace VehicleToll.Core.Application.Dates;

public interface ITollFreeDatesService
{
    public bool IsTollFreeDate(DateTime date);
}
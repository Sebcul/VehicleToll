namespace VehicleToll.Core.Application.Calculators;

public class TollSession
{
    private DateTime _startTime;

    public TollSession(DateTime startTime, int initialFee)
    {
        _startTime = startTime;
        MaxFee = initialFee;
    }

    public int MaxFee { get; private set; }

    public bool IsIn60MinutesInterval(DateTime date) =>
        (date - _startTime).TotalMinutes <= 60;

    public void UpdateFee(int fee)
    {
        MaxFee = Math.Max(MaxFee, fee);
    }
}
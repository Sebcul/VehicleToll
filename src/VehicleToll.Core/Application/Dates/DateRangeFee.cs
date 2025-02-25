namespace VehicleToll.Core.Application.Dates;

public class DateRangeFee
{
    public static readonly List<DateRangeFee> TollFeeRanges =
    [
        new DateRangeFee(new TimeSpan(6, 0, 0), new TimeSpan(6, 30, 0), 8),
        new DateRangeFee(new TimeSpan(6, 30, 0), new TimeSpan(7, 0, 0), 13),
        new DateRangeFee(new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), 18),
        new DateRangeFee(new TimeSpan(8, 0, 0), new TimeSpan(8, 30, 0), 13),
        new DateRangeFee(new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0), 8),
        new DateRangeFee(new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0), 13),
        new DateRangeFee(new TimeSpan(15, 30, 0), new TimeSpan(17, 0, 0), 18),
        new DateRangeFee(new TimeSpan(17, 0, 0), new TimeSpan(18, 0, 0), 13),
        new DateRangeFee(new TimeSpan(18, 0, 0), new TimeSpan(18, 30, 0), 8)
    ];
    
    public DateRangeFee(TimeSpan start, TimeSpan end, int fee)
    {
        Start = start;
        End = end;
        Fee = fee;
    }
    
    public TimeSpan Start { get; }
    public TimeSpan End { get; }
    public int Fee { get; }



    public bool Contains(TimeSpan time)
    {
        return time >= Start && time < End;
    }
}
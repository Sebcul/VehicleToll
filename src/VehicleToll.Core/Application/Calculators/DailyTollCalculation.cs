using VehicleToll.Core.Domain;

namespace VehicleToll.Core.Application.Calculators;

public class DailyTollCalculation
{
    private const int DailyMaxFee = 60;
    private int _totalFee;
    private TollSession? _currentSession;

    public void ProcessFee(DateTime date, int fee)
    {
        if (fee == 0)
        {
            return;
        }

        if (_currentSession == null)
        {
            _currentSession = new TollSession(date, fee);
        }
        else if (_currentSession.IsIn60MinutesInterval(date))
        {
            _currentSession.UpdateFee(fee);
        }
        else
        {
            _totalFee += _currentSession.MaxFee;
            _currentSession = new TollSession(date, fee);
        }
    }

    public int CalculateFinalFee()
    {
        if (_currentSession != null)
        {
            _totalFee += _currentSession.MaxFee;
        }

        return Math.Min(_totalFee, DailyMaxFee);
    }
}
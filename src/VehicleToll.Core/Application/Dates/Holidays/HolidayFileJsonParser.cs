using System.Text.Json;
using VehicleToll.Core.Application.Dates.Holidays.DTOs;

namespace VehicleToll.Core.Application.Dates.Holidays;

public class HolidayFileJsonParser : IHolidayFileParser
{
    public List<YearHolidaysDTO> ReadData(string directory)
    {
        var filePath = Path.Combine(directory, "swedish_holidays.json");
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<YearHolidaysDTO>>(json) ?? new List<YearHolidaysDTO>();
    }
}
using System.Text.Json.Serialization;

namespace VehicleToll.Core.Application.Dates.Holidays.DTOs;

public class YearHolidaysDTO
{
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("dates")]
    public List<HolidayDTO> Dates { get; set; }
}
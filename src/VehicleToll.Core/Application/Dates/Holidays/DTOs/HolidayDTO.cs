using System.Text.Json.Serialization;

namespace VehicleToll.Core.Application.Dates.Holidays.DTOs;

public class HolidayDTO
{
    [JsonPropertyName("date")]
    public string Date { get; set; }
}
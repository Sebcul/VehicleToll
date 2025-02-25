using VehicleToll.Core.Application.Calculators;
using VehicleToll.Core.Application.Dates.Holidays;
using VehicleToll.Core.Domain;

var holidayFileParser = new HolidayFileJsonParser();
var holidayService = new HolidayService(holidayFileParser);
var tollCalculator = new TollCalculator(holidayService);

tollCalculator.GetTollFee(new Car(), DateTime.Now);
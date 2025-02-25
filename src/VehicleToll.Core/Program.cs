using VehicleToll.Core.Application.Calculators;
using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Application.Dates.Holidays;
using VehicleToll.Core.Domain;

var holidayFileParser = new HolidayFileJsonParser();
var holidayService = new HolidayService(holidayFileParser);
var tollFreeDatesService = new TollFreeDatesService(holidayService);

var tollCalculator = new TollCalculator(tollFreeDatesService);

tollCalculator.GetTollFee(new Car(), DateTime.Now);
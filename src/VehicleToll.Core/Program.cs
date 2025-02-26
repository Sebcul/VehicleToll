using VehicleToll.Core.Application.Calculators;
using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Application.Dates.Holidays;
using VehicleToll.Core.Domain;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Application started..");

var holidayFileParser = new HolidayFileJsonParser();
var holidayService = new HolidayService(holidayFileParser);
var tollFreeDatesService = new TollFreeDatesService(holidayService);

var tollCalculator = new TollCalculator(tollFreeDatesService);

var singleFee = tollCalculator.GetTollFee(new Car(), DateTime.Now);


var date1 = new DateTime(2025, 1, 10, 6, 15, 0, 0);
var date2 = new DateTime(2025, 1, 10, 6, 45, 0, 0);
var date3 = new DateTime(2025, 1, 10, 8, 15, 0, 0);
var date4 = new DateTime(2025, 1, 10, 8, 45, 0, 0);
var date5 = new DateTime(2025, 1, 10, 16, 45, 0, 0);
var date6 = new DateTime(2025, 1, 10, 16, 30, 0, 0);
var date7 = new DateTime(2025, 1, 10, 17, 10, 0, 0);
var date8 = new DateTime(2025, 1, 10, 21, 10, 0, 0);

var dates = new[] { date1, date2, date3, date4, date5, date6, date7, date8 };
var multipleFees = tollCalculator.GetTollFee(new Car(), dates);

logger.LogInformation("Single fee: {fee}", singleFee);
logger.LogInformation("Multiple fees: {multipleFees}", multipleFees);
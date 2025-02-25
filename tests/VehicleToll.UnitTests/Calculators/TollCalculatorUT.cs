using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using VehicleToll.Core.Application.Calculators;
using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Application.Dates.Holidays;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Abstractions;
using VehicleToll.UnitTests.Common;

namespace VehicleToll.UnitTests.Calculators;

public class TollCalculatorUT
{
    private readonly TollCalculator _sutCalculator;
    public static IEnumerable<object[]> TollFreeVehicles = InlineData.TollFreeVehicles;
    private readonly int _year = 2025;

    public TollCalculatorUT()
    {
        var holidayServiceMock = new Mock<IHolidayService>();
        holidayServiceMock.Setup(x => x.GetHolidaysForYear(_year)).Returns(Data.Holidays2025.ToList());
        var tollFreeDateServiceMock = new Mock<TollFreeDatesService>(holidayServiceMock.Object);
        _sutCalculator = new TollCalculator(tollFreeDateServiceMock.Object);
    }


    [Theory]
    [InlineData(6, 15, 8)]
    [InlineData(6, 45, 13)]
    [InlineData(7, 0, 18)]
    [InlineData(8, 15, 13)]
    [InlineData(8, 45, 8)]
    [InlineData(15, 15, 13)]
    [InlineData(15, 45, 18)]
    [InlineData(16, 30, 18)]
    [InlineData(17, 30, 13)]
    [InlineData(18, 15, 8)]
    [InlineData(19, 0, 0)]
    public void GetTollFee_DateTime_Should_ReturnExpectedFee(int hour, int minute, int expectedFee)
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(_year, 1, 10, hour, minute, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(expectedFee, fee);
    }

    [Theory]
    [MemberData(nameof(TollFreeVehicles))]
    public void GetTollFee_DateTime_Should_ReturnZeroForTollFreeVehicle(IVehicle vehicle)
    {
        // Arrange
        var testDate = new DateTime(_year, 1, 10, 8, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_DateTime_Should_ReturnZeroFeeForTollFreeDateForSunday()
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(_year, 3, 2, 10, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_DateTime_Should_ReturnZeroFeeForTollFreeDateForSaturday()
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(_year, 3, 1, 10, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 6)]
    [InlineData(4, 17)]
    [InlineData(4, 19)]
    [InlineData(4, 20)]
    public void GetTollFee_DateTime_Should_ReturnZeroFeeForTollFreeDateForHolidays(int month, int day)
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(_year, month, day, 10, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_DateTime_Should_ReturnZeroForTollFreeMonth()
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(_year, 7, 10, 10, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_Daily_Should_GroupTollFeesWithin60Minutes()
    {
        // Arrange
        var vehicle = new Car();

        var date1 = new DateTime(_year, 1, 10, 6, 15, 0, 0);
        var date2 = new DateTime(_year, 1, 10, 6, 45, 0, 0);
        var dates = new[] { date1, date2 };

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(13, fee);
    }

    [Fact]
    public void GetTollFee_Daily_Should_GroupTollFeesWithin60MinutesForAllDay()
    {
        // Arrange
        var vehicle = new Car();

        var date1 = new DateTime(_year, 1, 10, 6, 15, 0, 0);
        var date2 = new DateTime(_year, 1, 10, 6, 45, 0, 0);
        var date3 = new DateTime(_year, 1, 10, 8, 15, 0, 0);
        var date4 = new DateTime(_year, 1, 10, 8, 45, 0, 0);
        var date5 = new DateTime(_year, 1, 10, 16, 45, 0, 0);
        var date6 = new DateTime(_year, 1, 10, 16, 30, 0, 0);
        var date7 = new DateTime(_year, 1, 10, 17, 10, 0, 0);
        var date8 = new DateTime(_year, 1, 10, 21, 10, 0, 0);

        var dates = new[] { date1, date2, date3, date4, date5, date6, date7, date8 };

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(44, fee);
    }

    [Fact]
    public void GetTollFee_Daily_ShouldNot_GroupTollFeesOver60Minutes()
    {
        // Arrange
        var vehicle = new Car();

        var date1 = new DateTime(_year, 1, 10, 6, 15, 0, 0);
        var date2 = new DateTime(_year, 1, 10, 8, 45, 0, 0);
        var dates = new[] { date1, date2 };

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(16, fee);
    }

    [Fact]
    public void GetTollFee_Daily_Should_CapTotalFeeAtSixty()
    {
        // Arrange
        var vehicle = new Car();


        var passes = new DateTime[10];
        for (int i = 0; i < 10; i++)
        {
            passes[i] = new DateTime(_year, 1, 10, 7, 0, 0, 0);
        }

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, passes);

        // Assert
        Assert.True(fee <= 60);
    }
}
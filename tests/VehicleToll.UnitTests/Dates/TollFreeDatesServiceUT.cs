using System;
using System.Collections.Generic;
using Moq;
using VehicleToll.Core.Application.Dates;
using VehicleToll.Core.Application.Dates.Holidays;

namespace VehicleToll.UnitTests.Dates;

public class TollFreeDatesServiceUT
{

    private readonly Mock<IHolidayService> _holidayServiceMock;
    private TollFreeDatesService _service;

    public TollFreeDatesServiceUT()
    {
        _holidayServiceMock = new Mock<IHolidayService>();
        _holidayServiceMock.Setup(s => s.GetHolidaysForYear(It.IsAny<int>()))
                           .Returns(new List<DateTime>());
        _service = new TollFreeDatesService(_holidayServiceMock.Object);
    }

    [Fact]
    public void IsTollFreeDate_OnSaturday_ShouldReturnTrue()
    {
        // Arrange
        var saturday = new DateTime(2025, 3, 1);

        // Act
        var result = _service.IsTollFreeDate(saturday);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTollFreeDate_OnSunday_ShouldReturnTrue()
    {
        // Arrange
        var sunday = new DateTime(2025, 3, 2);

        // Act
        var result = _service.IsTollFreeDate(sunday);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    [InlineData(25)]
    [InlineData(30)]
    public void IsTollFreeDate_InJuly_ShouldReturnTrue(int day)
    {
        // Arrange
        var julyDate = new DateTime(2025, 7, day);

        // Act
        var result = _service.IsTollFreeDate(julyDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTollFreeDate_OnHoliday_ShouldReturnTrue()
    {
        // Arrange
        var holidayDate = new DateTime(2025, 4, 18);
        var holidays = new List<DateTime> { holidayDate };
        _holidayServiceMock.Setup(s => s.GetHolidaysForYear(2025))
                           .Returns(holidays);
        _service = new TollFreeDatesService(_holidayServiceMock.Object);

        // Act
        var result = _service.IsTollFreeDate(holidayDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTollFreeDate_DayBeforeHoliday_ShouldReturnTrue()
    {
        // Arrange
        var holidayDate = new DateTime(2025, 4, 18);
        var holidays = new List<DateTime> { holidayDate };

        _holidayServiceMock.Setup(s => s.GetHolidaysForYear(2025))
                           .Returns(holidays);
        _service = new TollFreeDatesService(_holidayServiceMock.Object);
        var dayBeforeHoliday = holidayDate.AddDays(-1);

        // Act
        var result = _service.IsTollFreeDate(dayBeforeHoliday);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTollFreeDate_NonTollFreeWeekday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2025, 5, 5);

        // Act
        var result = _service.IsTollFreeDate(date);

        // Assert
        Assert.False(result);
    }
}
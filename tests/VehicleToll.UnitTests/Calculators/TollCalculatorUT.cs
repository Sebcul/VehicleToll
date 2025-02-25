using System;
using System.Collections.Generic;
using VehicleToll.Core.Application.Calculators;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Abstractions;
using VehicleToll.UnitTests.Common;

namespace VehicleToll.UnitTests.Calculators;

public class TollCalculatorUT
{

    private readonly TollCalculator _sutCalculator;

    public static IEnumerable<object[]> TollFreeVehiclesInlineData =>
        new List<IVehicle[]>
        {
            new IVehicle[] { new Motorbike() },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Diplomat) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Emergency) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Foreign) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Military) },
            new IVehicle[] { new FakeVehicle(VehicleTypeConstants.Tractor) }
        };

    public TollCalculatorUT()
    {
        _sutCalculator = new TollCalculator();
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
        var testDate = new DateTime(2023, 1, 10, hour, minute, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(expectedFee, fee);
    }

    [Theory]
    [MemberData(nameof(TollFreeVehiclesInlineData))]
    public void GetTollFee_DateTime_Should_ReturnZeroForTollFreeVehicle(IVehicle vehicle)
    {
        // Arrange
        var testDate = new DateTime(2023, 1, 10, 8, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_DateTime_Should_ReturnZeroForTollFreeDateForSunday()
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(2025, 3, 2, 10, 0, 0);

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, testDate);

        // Assert
        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_DateTime_Should_ReturnZeroForTollFreeDateForSaturday()
    {
        // Arrange
        var vehicle = new Car();
        var testDate = new DateTime(2025, 3, 1, 10, 0, 0);

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
        var testDate = new DateTime(2025, 7, 10, 10, 0, 0);

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

        var date1 = new DateTime(2023, 1, 10, 6, 15, 0, 0);
        var date2 = new DateTime(2023, 1, 10, 6, 45, 0, 0);
        var dates = new[] { date1, date2 };

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, dates);

        // Assert
        Assert.Equal(13, fee);
    }
    
    [Fact]
    public void GetTollFee_Daily_ShouldNot_GroupTollFeesOver60Minutes()
    {
        // Arrange
        var vehicle = new Car();

        var date1 = new DateTime(2023, 1, 10, 6, 15, 0, 0);
        var date2 = new DateTime(2023, 1, 10, 8, 45, 0, 0);
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
            passes[i] = new DateTime(2023, 1, 10, 7, 0, 0, 0);
        }

        // Act
        var fee = _sutCalculator.GetTollFee(vehicle, passes);

        // Assert
        Assert.True(fee <= 60);
    }
}
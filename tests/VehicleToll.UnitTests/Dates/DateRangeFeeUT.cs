using System;
using VehicleToll.Core.Application.Dates;

namespace VehicleToll.UnitTests.Dates;

public class DateRangeFeeUT
{

    [Fact]
    public void TollFeeRanges_Should_HaveNineEntries()
    {
        //Arrange, Act
        var count = DateRangeFee.TollFeeRanges.Count;

        //Assert
        Assert.Equal(9, count);
    }

    [Theory]
    [InlineData(0, "06:29:59", true)]
    [InlineData(0, "06:30:00", false)]
    [InlineData(1, "06:29:59", false)]
    [InlineData(1, "06:30:00", true)]
    [InlineData(1, "06:59:59", true)]
    [InlineData(1, "07:00:00", false)]
    [InlineData(2, "07:00:00", true)]
    [InlineData(2, "07:30:00", true)]
    [InlineData(2, "08:00:00", false)]
    public void Contains_RangeBoundaries_Should_ReturnExpected(int rangeIndex, string timeString, bool expected)
    {
        //Arrange
        TimeSpan time = TimeSpan.Parse(timeString);
        var range = DateRangeFee.TollFeeRanges[rangeIndex];

        //Act
        var result = range.Contains(time);

        //Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, "06:15:00", 8)]
    [InlineData(1, "06:45:00", 13)]
    [InlineData(2, "07:30:00", 18)]
    [InlineData(3, "08:15:00", 13)]
    [InlineData(4, "09:00:00", 8)]
    [InlineData(5, "15:15:00", 13)]
    [InlineData(6, "16:00:00", 18)]
    [InlineData(7, "17:30:00", 13)]
    [InlineData(8, "18:15:00", 8)]
    public void TollFeeRanges_Should_ReturnCorrectFee_WhenTimeIsWithinRange(int rangeIndex, string timeString, int expectedFee)
    {
        //Arrange
        TimeSpan time = TimeSpan.Parse(timeString);
        var range = DateRangeFee.TollFeeRanges[rangeIndex];

        //Act
        var contains = range.Contains(time);

        //Assert
        Assert.True(contains);
        Assert.Equal(expectedFee, range.Fee);
    }
}
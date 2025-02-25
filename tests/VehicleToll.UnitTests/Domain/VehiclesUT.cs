using System.Collections.Generic;
using VehicleToll.Core.Domain;
using VehicleToll.Core.Domain.Abstractions;
using VehicleToll.UnitTests.Common;

namespace VehicleToll.UnitTests.Domain;

public class VehiclesUT
{
    public static IEnumerable<object[]> TollFreeVehicles = InlineData.TollFreeVehicles;

    [Theory]
    [MemberData(nameof(TollFreeVehicles))]
    public void IsTollFree_Should_ReturnTrueWhenVehicleIsOfTollFreeType(IVehicle vehicle)
    {
        Assert.True(vehicle.IsTollFree());
    }
    
    [Fact]
    public void IsTollFree_Should_ReturnFalseWhenVehicleIsNotOfTollFreeType()
    {
        //Arrange
        var vehicle = new Car();
        
        //Act, Assert
        Assert.False(vehicle.IsTollFree());
    }
}
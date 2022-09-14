using Coordinator;

namespace UnitTests;/// <summary>
                    /// //
                    /// </summary>

public class CoordinateTests
{
    public const int DEGREES = 79;
    public const int MINUTES = 34;
    public const int SECONDS = 19;

    public const float D_AS_WGS84 = 79.0f;
    public const float DM_AS_WGS84 = 79.566667f;
    public const float DMS_AS_WGS84 = 79.571944f;

    public const int NAUTICAL_MILES = 20;
    
    [Theory]
    [InlineData(DEGREES, 0, 0, D_AS_WGS84)]
    [InlineData(DEGREES, MINUTES, 0, DM_AS_WGS84)]
    [InlineData(DEGREES, MINUTES, SECONDS, DMS_AS_WGS84)]
    public void GivenDegrees_WhenConvertedToWGS84_ThenReturnsDegrees(int degrees, int minutes, int seconds, float wgs84)
    {
        var coordinate = new Coordinate(degrees, minutes, seconds);
        Assert.Equal(wgs84, coordinate.WGS84Coordinate);
    }

    [Fact]
    public void GivenPositiveLatitude_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var coordinate = new Coordinate(DEGREES, MINUTES, SECONDS);
        var newCoordinate = coordinate.AddNauticalMilesToLatitude(NAUTICAL_MILES);
        var expected = new Coordinate(DEGREES, MINUTES + NAUTICAL_MILES, SECONDS);
        Assert.Equal(expected.WGS84Coordinate, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOnEquator_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(0);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(0.33273f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOn45DegreesLatitude_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(45);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(0.47055125f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOn40DegreesLatitude_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(40);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(0.43434817f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOn70DegreesLatitude_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(70);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(0.9728374f, newCoordinate.WGS84Coordinate);
    }
    
        
    [Fact]
    public void GivenLongitudeOnNegative80DegreesLatitude_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(-80);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(1.9161156f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOnNegative90DegreesLatitude_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(-90);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(0f, newCoordinate.WGS84Coordinate);
    }

    [Fact]
    public void GivenLongitudeOnEquator_WhenSubtractingNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(0);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.SubtractNauticalMilesFromLongitude(NAUTICAL_MILES, latitude);
        Assert.Equal(-0.33273f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOnEquator_WhenAddingMoreThanEarthCircumferenceOfNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(0);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.AddNauticalMilesToLongitude(22000.0f, latitude);
        Assert.Equal(6.0029907f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenLongitudeOnEquator_WhenSubtractingMoreThanEarthCircumferenceOfNauticalMiles_ThenReturnsNewCoordinate()
    {
        var latitude = new Coordinate(0);
        var coordinate = new Coordinate(0);
        var newCoordinate = coordinate.SubtractNauticalMilesFromLongitude(22000.0f, latitude);
        Assert.Equal(-6.0029907f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenPositiveLatitude_WhenAddingNauticalMilesBeyond90Degrees_ThenReturnsNewCoordinate()
    {
        var coordinate = new Coordinate(45);
        var newCoordinate = coordinate.AddNauticalMilesToLatitude(3300);
        Assert.Equal(80f, newCoordinate.WGS84Coordinate);
    }
    
    [Fact]
    public void GivenPositiveLatitude_WhenSubtractingNauticalMilesBeyondNegative90Degrees_ThenReturnsNewCoordinate()
    {
        var coordinate = new Coordinate(45);
        var newCoordinate = coordinate.SubtractNauticalMilesFromLatitude(8700);
        Assert.Equal(-80f, newCoordinate.WGS84Coordinate);
    }
}
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
    
    // [Fact]
    // public void GivenLongitudeOnEquator_WhenAddingNauticalMiles_ThenReturnsNewCoordinate()
    // {
    //     var coordinate = new Coordinate(0);
    //     var newCoordinate = coordinate.AddLongitudinalNauticalMiles();
    // }
}
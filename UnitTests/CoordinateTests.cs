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
    
    [Theory]
    [InlineData(DEGREES, 0, 0, D_AS_WGS84)]
    [InlineData(DEGREES, MINUTES, 0, DM_AS_WGS84)]
    [InlineData(DEGREES, MINUTES, SECONDS, DMS_AS_WGS84)]
    public void GivenDegrees_WhenConvertedToWGS84_ThenReturnsDegrees(int degrees, int minutes, int seconds, float wgs84)
    {
        var coordinate = new Coordinate(degrees, minutes, seconds);
        Assert.Equal(wgs84, coordinate.WGS84Coordinate);
    }
}
using ADSBLookup;

namespace UnitTests;

public class ConversionTests
{
    [Theory]
    [InlineData(1, ConversionConstant.FEET_PER_METER)]
    [InlineData(-1, -ConversionConstant.FEET_PER_METER)]
    [InlineData(10000, 32800)]
    public void GivenAMeasurementInMeter_WhenCalculatingFeet_ThenReturnsConvertedValue(float meters, float convertedFeet)
    {
        float feet = meters * ConversionConstant.FEET_PER_METER;
        Assert.Equal(convertedFeet, feet);
    }

    [Theory]
    [InlineData(1, ConversionConstant.KNOTS_FROM_METERS_PER_SECOND)]
    [InlineData(0, 0)]
    [InlineData(240, 466.52255)]
    public void GivenASpeedInMetersPerSecond_WhenCalculatingKnots_ThenReturnsConvertedValue(float ms, float convertedKnots)
    {
        float knots = ms * ConversionConstant.KNOTS_FROM_METERS_PER_SECOND;
        Assert.Equal(convertedKnots, knots);
    }
}
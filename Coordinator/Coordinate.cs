namespace Coordinator;

public class Coordinate
{
    // https://www.thoughtco.com/degree-of-latitude-and-longitude-distance-4070616
    private const float NM_PER_DEGREE_LATITUDE = 60f;
    private const float NM_PER_DEGREE_LONGITUDE_AT_EQUATOR = 60.1088f;

    public Coordinate(float coordinate)
    {
        this.WGS84Coordinate = coordinate;
    }

    public Coordinate(int degrees, int minutes, int seconds)
    {
        float decimalDegrees = degrees;
        decimalDegrees += minutes / 60.0f;
        decimalDegrees += seconds / 3600.0f;
        this.WGS84Coordinate = decimalDegrees;
    }

    public Coordinate AddNauticalMilesToLatitude(float nauticalMiles)
    {
        float degrees = nauticalMiles / NM_PER_DEGREE_LATITUDE;
        float newDegrees = this.WGS84Coordinate + degrees;

        return new Coordinate(NormalizeLatitude(newDegrees));
    }

    public Coordinate SubtractNauticalMilesFromLatitude(float nauticalMiles)
    {
        float degrees = nauticalMiles / NM_PER_DEGREE_LATITUDE;
        float newDegrees = this.WGS84Coordinate - degrees;

        return new Coordinate(NormalizeLatitude(newDegrees));
    }

    public static float NormalizeLatitude(float degrees)
    {
        if (degrees > 180f)
        {
            degrees %= 180f;
        } 
        else if (degrees < -180f)
        {
            degrees %= 180f;
        }

        if (degrees > 90)
        {
            degrees = 180f - degrees;
        }
        else if (degrees < -90f)
        {
            degrees = 180 + degrees;
            degrees *= -1f;
        }
        return degrees;
    }

    public Coordinate AddNauticalMilesToLongitude(float nauticalMiles, Coordinate latitude)
    {
        float newDegrees;
        if (latitude.WGS84Coordinate == 0)
        {
            float degrees = (float)(nauticalMiles / NM_PER_DEGREE_LONGITUDE_AT_EQUATOR); 
            newDegrees = this.WGS84Coordinate + degrees;
        }
        else if (latitude.WGS84Coordinate == 90 || latitude.WGS84Coordinate == -90)
        {
            newDegrees = 0;
        }
        else
        {
            double radians = Math.PI * latitude.WGS84Coordinate / 180.0;
            double nmPerDegree = NM_PER_DEGREE_LONGITUDE_AT_EQUATOR * Math.Cos(radians);
            float degrees = (float)(nauticalMiles / nmPerDegree); 
            newDegrees = this.WGS84Coordinate + degrees;     
        }
        
        return new Coordinate(NormalizeLongitude(newDegrees));
    }

    public static float NormalizeLongitude(float degrees)
    {
        if (degrees < 0)
        {
            float absoluteDegrees = NormalizeLongitude(degrees * -1f);
            degrees = absoluteDegrees == 180f ? absoluteDegrees : absoluteDegrees * -1;
        }
        else
        {
            if (degrees >= 360f)
            {
                degrees %= 360.0f;
            }

            if (degrees > 180f)
            {
                degrees -= 180.0f;
                degrees *= -1f;
            }
        }

        return degrees;
    }

    public Coordinate SubtractNauticalMilesFromLongitude(float nauticalMiles, Coordinate latitude)
    {
        return AddNauticalMilesToLongitude(-nauticalMiles, latitude);
    }

    public float WGS84Coordinate { get; private set; }
}
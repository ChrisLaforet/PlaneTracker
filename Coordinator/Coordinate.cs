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
        if (newDegrees >= 0 && newDegrees > 90f)
        {
            newDegrees = newDegrees - (newDegrees - 90f);
        }
        else if (newDegrees < 0 && newDegrees < -90f)
        {
            newDegrees = newDegrees + (newDegrees + 90f);
        }

        return new Coordinate(newDegrees);
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

        return new Coordinate(newDegrees);
    }


    public float WGS84Coordinate { get; private set; }
}
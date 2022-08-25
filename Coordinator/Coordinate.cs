namespace Coordinator;

public class Coordinate
{
    private const float NM_PER_DEGREE_LATITUDE = 60;
     
    public Coordinate(float coordinate) {
        this.WGS84Coordinate = coordinate;
    }
    
    public Coordinate(int degrees, int minutes, int seconds) {
        float decimalDegrees = degrees;
        decimalDegrees += minutes / 60.0f;
        decimalDegrees += seconds / 3600.0f;
        this.WGS84Coordinate = decimalDegrees;
    }
    
    public Coordinate AddNauticalMilesToLatitude(float nauticalMiles) {
        float degrees = nauticalMiles / NM_PER_DEGREE_LATITUDE;
        float newDegrees = this.WGS84Coordinate + degrees;
        if (newDegrees >= 0 && newDegrees > 90f) {
            newDegrees = newDegrees - (newDegrees - 90f);
        } else if (newDegrees < 0 && newDegrees < -90f) {
            newDegrees = newDegrees + (newDegrees + 90f);
        }
        return new Coordinate(newDegrees);
    }

    
    public float WGS84Coordinate { get; private set; }
    
    
}
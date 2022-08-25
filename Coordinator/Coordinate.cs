namespace Coordinator;

public class Coordinate
{
    public Coordinate(float coordinate) {
        this.WGS84Coordinate = coordinate;
    }
    
    public Coordinate(int degrees, int minutes, int seconds) {
        float decimalDegrees = degrees;
        decimalDegrees += minutes / 60.0f;
        decimalDegrees += seconds / 3600.0f;
        this.WGS84Coordinate = decimalDegrees;
    }
    
    public float WGS84Coordinate { get; private set; }
    
    
}
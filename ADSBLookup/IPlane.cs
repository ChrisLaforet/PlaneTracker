namespace ADSBLookup;

public interface IPlane
{
    public string ICAO24 { get; }
    public string CallSign { get; }
    public string OriginCountry { get; }
    public int TimePosition { get; }
    public int LastContact { get; }
    public float Longitude { get; }
    public float Latitude { get; }
    public float? BarometricAltitudeInFeet { get; }
    public float? TrueTrack { get; }
    public bool OnGround { get; }
    public float? VerticalRateInFeetPerSecond { get; }
    public float? VelocityInKnots { get; }
    public float? AltitudeInFeet { get; }
    public string Squawk { get; }
    public bool Spi { get; }
    
    public PositionSourcing PositionSourcing { get; }
    public PlaneType PlaneType { get;  }
}
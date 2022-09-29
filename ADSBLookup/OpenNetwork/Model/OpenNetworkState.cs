namespace ADSBLookup.OpenNetwork.Model;

public class OpenNetworkState : IPlane
{
    public string ICAO24 { get; set; }
    public string CallSign { get; set; }
    public string OriginCountry { get; set; }
    public int TimePosition { get; set; }
    public int LastContact { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float BaroAltitude { get; set; }
    public bool OnGround { get; set; }
    public float Velocity { get; set; }
    public float? TrueTrack { get; set; }
    public float? VerticalRate { get; set; }
    public IEnumerable<int> sensors { get; set; }
    public float GeoAltitude { get; set; }
    public string Squawk { get; set; }
    public bool Spi { get; set; }
    public int PositionSource { get; set; }
    public int? Category { get; set; }
}
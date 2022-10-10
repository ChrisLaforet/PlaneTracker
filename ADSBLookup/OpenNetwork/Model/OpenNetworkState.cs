namespace ADSBLookup.OpenNetwork.Model;

public class OpenNetworkState : IPlane
{
    public string ICAO24 { get; set; }
    public string? CallSign { get; set; }
    public string OriginCountry { get; set; }
    public int TimePosition { get; set; }
    public int LastContact { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float? BaroAltitude { get; set; }

    public float? BarometricAltitudeInFeet
    {
        get
        {
            return BaroAltitude == null ? null : BaroAltitude * ConversionConstant.FEET_PER_METER;
        }
    }

    public bool OnGround { get; set; }
    public float? Velocity { get; set; }

    public float? VelocityInKnots
    {
        get
        {
            return Velocity == null ? null : Velocity * ConversionConstant.KNOTS_FROM_METERS_PER_SECOND;
        }
    }

    public float? TrueTrack { get; set; }
    public float? VerticalRate { get; set; }

    public float? VerticalRateInFeetPerSecond
    {
        get
        {
            return VerticalRate == null ? null : VerticalRate * ConversionConstant.FEET_PER_METER;
        }
    }

    public IEnumerable<int> sensors { get; set; }
    public float? GeoAltitude { get; set; }

    public float? AltitudeInFeet
    {
        get
        {
            return GeoAltitude == null ? null : GeoAltitude * ConversionConstant.FEET_PER_METER;
        }
    }

    public string Squawk { get; set; }
    public bool Spi { get; set; }
    public int PositionSource { get; set; }
    public int? Category { get; set; }

    public PositionSourcing PositionSourcing
    {
        get
        {
            return PositionSource == 0 ? PositionSourcing.ADSB : PositionSourcing.Other;
        }
    }

    public PlaneType PlaneType
    {
        get
        {
            if (Category == null)
            {
                return PlaneType.Unknown;
            }

            switch ((int)Category)
            {
                case 0: 
                case 1:
                    return PlaneType.Unknown;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    return PlaneType.Plane;
                case 8:
                    return PlaneType.Rotorcraft;
                case 9:
                    return PlaneType.Glider;
                case 10:
                    return PlaneType.LighterThanAir;
                case 11:
                    return PlaneType.Skydiver;
                case 12:
                    return PlaneType.Ultralight;
                case 14:
                    return PlaneType.UAV;
                default:
                    return PlaneType.Other;
            }
        }
        
    }
}
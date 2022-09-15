using System.Text.Json.Serialization;

namespace ADSBLookup.OpenNetwork.Model;

public class OpenNetworkState
{
    public string ICAO24 { get; set; }
    public string CallSign { get; set; }
    //
    // [JsonPropertyName("origin_country")]
    // public string OriginCountry { get; set; }
    //
    // [JsonPropertyName("time_position")]
    // public int TimePosition { get; set; }
    //
    // [JsonPropertyName("last_contact")]
    // public int LastContact { get; set; }
    //
    // public float Longitude { get; set; }
    // public float Latitude { get; set; }
    //
    // [JsonPropertyName("baro_altitude")]
    // public float BaroAltitude { get; set; }
    //
    // [JsonPropertyName("on_ground")]
    // public bool OnGround { get; set; }
    //
    // public float Velocity { get; set; }
    //
    // [JsonPropertyName("true_track")]
    // public float? TrueTrack { get; set; }
    //
    // [JsonPropertyName("vertical_rate")]
    // public float? VerticalRate { get; set; }
    //
    // public IEnumerable<int> sensors { get; set; }
    //
    // [JsonPropertyName("geo_altitude")]
    // public float GeoAltitude { get; set; }
    //
    // public string Squawk { get; set; }
    //
    // public bool Spi { get; set; }
    //
    // [JsonPropertyName("position_source")]
    // public int PositionSource { get; set; }
    //
    // public int? Category { get; set; }
}
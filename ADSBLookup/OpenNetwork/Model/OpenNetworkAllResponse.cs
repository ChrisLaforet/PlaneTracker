using System.Text.Json;
using System.Text.Json.Serialization;

namespace ADSBLookup.OpenNetwork.Model;

public class OpenNetworkAllResponse
{
    [JsonPropertyName("time")] 
    public int Time { get; set; }

    [JsonPropertyName("states")] 
    public JsonElement States { get; set; }
    
    private List<OpenNetworkState> state;
    
    public List<OpenNetworkState> State
    {
        get
        {
            if (state != null)
            {
                return state;
            }

            state = new List<OpenNetworkState>();

            var arrayEnumerator = States.EnumerateArray();
            while (arrayEnumerator.MoveNext())
            {
                var array = arrayEnumerator.Current;
                int index = 0;
                var record = new OpenNetworkState();

                var enumerator = array.EnumerateArray();
                while (enumerator.MoveNext())
                {
                    var property = enumerator.Current;
                    switch (index)
                    {
                        case 0:
                            record.ICAO24 = ExtractStringFrom(property);
                            break;
                        case 1:
                            record.CallSign = ExtractStringFrom(property).Trim();
                            break;
                        case 2:
                            record.OriginCountry = ExtractStringFrom(property);
                            break;
                        case 14:
                            record.Squawk = ExtractStringFrom(property);
                            break;
                        
                        case 5:
                            record.Longitude = (float)ExtractFloatFrom(property);
                            break;
                        case 6:
                            record.Latitude = (float)ExtractFloatFrom(property);
                            break;
                        case 7:
                            record.BaroAltitude = ExtractFloatFrom(property);
                            break;
                        case 9:
                            record.Velocity = ExtractFloatFrom(property);
                            break;
                        case 10:
                            record.TrueTrack = ExtractFloatFrom(property);
                            break;
                        case 11:
                            record.VerticalRate = ExtractFloatFrom(property);
                            break;
                        case 13:
                            record.GeoAltitude = ExtractFloatFrom(property);
                            break;
                        case 3:
                            record.TimePosition = (int)ExtractIntFrom(property);
                            break;
                        case 4:
                            record.LastContact = (int)ExtractIntFrom(property);
                            break;
                        case 16:
                            record.PositionSource = (int)ExtractIntFrom(property);
                            break;
                        case 17:
                            record.Category = ExtractIntFrom(property);
                            break;
                        
                        case 8:
                            record.OnGround = (bool)ExtractBoolFrom(property);
                            break;
                        case 15:
                            record.Spi = (bool)ExtractBoolFrom(property);
                            break;
                    }

                    ++index;
                }
                state.Add(record);
            }

            return state;
        }
    }

    private string ExtractStringFrom(JsonElement property)
    {
        var value = property.GetString();
        if (value == null)
        {
            return "";
        }

        return value;
    }
    
    private float? ExtractFloatFrom(JsonElement property)
    {
        var value = property.ToString();
        float result;
        if (float.TryParse(value, out result))
        {
            return result;
        }

        return null;    
    }

    private int? ExtractIntFrom(JsonElement property)
    {
        var value = property.ToString();
        int result;
        if (int.TryParse(value, out result))
        {
            return result;
        }

        return null;  
    }

    private bool? ExtractBoolFrom(JsonElement property)
    {
        var value = property.ToString().ToLower();
        if (value == "true" || value == "false")
        {
            return value == "true";
        }
        return null;
    }
}
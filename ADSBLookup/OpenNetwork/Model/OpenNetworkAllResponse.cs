using System.Text.Json;
using System.Text.Json.Serialization;

namespace ADSBLookup.OpenNetwork.Model;

public class OpenNetworkAllResponse
{
    [JsonPropertyName("time")] 
    public int Time { get; set; }

    [JsonPropertyName("states")] 
    public JsonElement States { get; set; }
    
    private OpenNetworkState state;
    
    public OpenNetworkState State
    {
        get
        {
            if (state != null)
            {
                return state;
            }

            state = new OpenNetworkState();
            var arrayEnumerator = States.EnumerateArray();
            while (arrayEnumerator.MoveNext())
            {
                var array = arrayEnumerator.Current;
                int index = 0;
                var enumerator = array.EnumerateArray();
                while (enumerator.MoveNext())
                {
                    var property = enumerator.Current;
                    switch (index)
                    {
                        case 0:
                            state.ICAO24 = ExtractStringFrom(property);
                            break;
                        case 1:
                            state.CallSign = ExtractStringFrom(property).Trim();
                            break;
                        case 2:
                            state.OriginCountry = ExtractStringFrom(property);
                            break;
                        case 14:
                            state.Squawk = ExtractStringFrom(property);
                            break;
                        
                        case 5:
                            state.Longitude = (float)ExtractFloatFrom(property);
                            break;
                        case 6:
                            state.Latitude = (float)ExtractFloatFrom(property);
                            break;
                        case 7:
                            state.BaroAltitude = (float)ExtractFloatFrom(property);
                            break;
                        case 9:
                            state.Velocity = (float)ExtractFloatFrom(property);
                            break;
                        case 10:
                            state.TrueTrack = ExtractFloatFrom(property);
                            break;
                        case 11:
                            state.VerticalRate = ExtractFloatFrom(property);
                            break;
                        case 13:
                            state.GeoAltitude = (float)ExtractFloatFrom(property);
                            break;
                        
                        case 3:
                            state.TimePosition = (int)ExtractIntFrom(property);
                            break;
                        case 4:
                            state.LastContact = (int)ExtractIntFrom(property);
                            break;
                        case 16:
                            state.PositionSource = (int)ExtractIntFrom(property);
                            break;
                        case 17:
                            state.Category = ExtractIntFrom(property);
                            break;
                        
                        case 8:
                            state.OnGround = (bool)ExtractBoolFrom(property);
                            break;
                        case 15:
                            state.Spi = (bool)ExtractBoolFrom(property);
                            break;
                    }

                    ++index;
                }
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
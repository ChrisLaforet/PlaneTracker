using System.Text.Json;
using System.Text.Json.Serialization;
using ADSBLookup.OpenNetwork.Model;

namespace ADSBLookup.OpenNetwork.Mapper;

public class OpenNetworkMapper
{
    public static OpenNetworkAllResponse? DecodeStatesAllResponse(string response)
    {
        return JsonSerializer.Deserialize<OpenNetworkAllResponse>(response, 
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
    }
    
}
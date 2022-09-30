using System.Net.Http.Headers;
using System.Text;
using ADSBLookup.OpenNetwork.Mapper;
using ADSBLookup.OpenNetwork.Model;
using Coordinator;

namespace ADSBLookup.OpenNetwork.Lookup;

public class OpenNetworkLookup : IPlaneLookup
{
    public const string OPEN_SKY_NETWORK_URL = "https://opensky-network.org/api/states/all";

    public List<IPlane> GetPlanesCenteredOn(Coordinate latitude, Coordinate longitude, float boxSideNM)
    {
        var response = GetPlanesFromOpenSkyNetwork(latitude, longitude, boxSideNM).Result;
        var planes = new List<IPlane>();
        if (response != null)
        {
            foreach (var openNetworkState in response.State)
            {
                planes.Add(openNetworkState);
            }
        }
        return planes;
    }

    private async Task<OpenNetworkAllResponse?> GetPlanesFromOpenSkyNetwork(Coordinate latitude, Coordinate longitude, float boxSideNM)
    {
        var latitudeMin = latitude.SubtractNauticalMilesFromLatitude(boxSideNM / 2.0f);
        var latitudeMax = latitude.AddNauticalMilesToLatitude(boxSideNM / 2.0f);
        var longitudeMin = longitude.SubtractNauticalMilesFromLongitude(boxSideNM / 2.0f, latitude);
        var longitudeMax = longitude.AddNauticalMilesToLongitude(boxSideNM / 2.0f, latitude);

        var queryString = new StringBuilder();
        queryString.Append($"?lamin={latitudeMin.WGS84Coordinate}");
        queryString.Append($"&lamax={latitudeMax.WGS84Coordinate}");
        queryString.Append($"&lomin={longitudeMin.WGS84Coordinate}");
        queryString.Append($"&lomax={longitudeMax.WGS84Coordinate}");
        queryString.Append("&extended=1");

        var client = new HttpClient();
        client.BaseAddress = new Uri(OPEN_SKY_NETWORK_URL);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync(queryString.ToString()).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return OpenNetworkMapper.DecodeStatesAllResponse(json);
        }

        return null;
    }
}
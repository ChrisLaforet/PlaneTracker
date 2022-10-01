using System.Text.Json;
using AirportLookup.FlightLabs.Model;

namespace AirportLookup.FlightLabs.Mapper;

public static class FlightLabsAirportResponseMapper
{
	public static List<FlightLabsAirport> DecodeAirportsFrom(string json)
	{
		return JsonSerializer.Deserialize<List<FlightLabsAirport>>(json,
			new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			});
	}
}
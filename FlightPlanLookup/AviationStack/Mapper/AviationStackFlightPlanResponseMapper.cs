using System.Text.Json;
using FlightPlanLookup.AviationStack.Model;

namespace FlightPlanLookup.AviationStack.Mapper;

public static class AviationStackFlightPlanResponseMapper
{
	public static AviationStackFlightPlans DecodeFlightPlansFrom(string json)
	{
		return JsonSerializer.Deserialize<AviationStackFlightPlans>(json,
			new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			});
	}
}
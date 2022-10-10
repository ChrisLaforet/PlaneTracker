using System.Net.Http.Headers;
using System.Text;
using FlightPlanLookup.AviationStack.Mapper;
using FlightPlanLookup.AviationStack.Model;

namespace FlightPlanLookup.AviationStack.Lookup;

public class AviationStackLookup : IFlightPlanLookup
{
	const string AVIATION_STACK_URL = "http://api.aviationstack.com/v1/flights";		// not https on free account!

	private string accessKey;

	public AviationStackLookup(string accessKey) => this.accessKey = accessKey;

	public List<IFlightPlan> GetFlightPlanForFlightICAO(string flightICAO)
	{
		if (string.IsNullOrEmpty(flightICAO))
		{
			return new List<IFlightPlan>();
		}
		return ExtractFlightPlansFromResponse(GetFlightPlansFromAviationStackByFlightICAO(flightICAO).Result);
	}

	private static List<IFlightPlan> ExtractFlightPlansFromResponse(AviationStackFlightPlans? response)
	{
		var flightPlans = new List<IFlightPlan>();
		if (response != null)
		{
			foreach (var plan in response.plans)
			{
				flightPlans.Add(plan.FlightPlan);
			}
		}

		return flightPlans;
	}

	public List<IFlightPlan> GetFlightPlanForFlightIATA(string flightIATA)
	{
		if (string.IsNullOrEmpty(flightIATA))
		{
			return new List<IFlightPlan>();
		}
		return ExtractFlightPlansFromResponse(GetFlightPlansFromAviationStackByFlightIATA(flightIATA).Result);
	}

	public List<IFlightPlan> GetFlightPlansForFlightsForAirportICAO(string airportICAO)
	{
		var flightPlans = new List<IFlightPlan>();
		flightPlans.AddRange(ExtractFlightPlansFromResponse(GetFlightPlansForAirportICAOFromAviationStack(airportICAO, true).Result));
		flightPlans.AddRange(ExtractFlightPlansFromResponse(GetFlightPlansForAirportICAOFromAviationStack(airportICAO, false).Result));
		return flightPlans;
	}
	
	public List<IFlightPlan> GetFlightPlansForFlightsForAirportIATA(string airportIATA)
	{
		var flightPlans = new List<IFlightPlan>();
		flightPlans.AddRange(ExtractFlightPlansFromResponse(GetFlightPlansForAirportIATAFromAviationStack(airportIATA, true).Result));
		flightPlans.AddRange(ExtractFlightPlansFromResponse(GetFlightPlansForAirportIATAFromAviationStack(airportIATA, false).Result));
		return flightPlans;
	}
	
	private async Task<AviationStackFlightPlans> GetFlightPlansFromAviationStackByFlightICAO(string flightICAO)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&airline_iata={flightICAO}");
// not supported by free plan (setting date)		
//		queryString.Append($"&flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		return await RequestFromAviationStack(queryString.ToString());
	}
	
	private async Task<AviationStackFlightPlans> GetFlightPlansFromAviationStackByFlightIATA(string flightIATA)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&airline_iata={flightIATA}");
// not supported by free plan (setting date)		
//		queryString.Append($"&flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		return await RequestFromAviationStack(queryString.ToString());
	}

	private async Task<AviationStackFlightPlans> GetFlightPlansForAirportICAOFromAviationStack(string airportICAO, bool isArrival)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		if (isArrival)
		{
			queryString.Append($"&dep_icao={airportICAO}");
		}
		else
		{
			queryString.Append($"&arr_icao={airportICAO}");
		}
// not supported by free plan (setting date)		
//		queryString.Append($"&flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		var client = new HttpClient();
		client.BaseAddress = new Uri(AVIATION_STACK_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		return await RequestFromAviationStack(queryString.ToString());
	}
	
	private async Task<AviationStackFlightPlans> GetFlightPlansForAirportIATAFromAviationStack(string airportIATA, bool isArrival)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		if (isArrival)
		{
			queryString.Append($"&dep_iata={airportIATA}");
		}
		else
		{
			queryString.Append($"&arr_iata={airportIATA}");
		}
// not supported by free plan (setting date)		
//		queryString.Append($"&flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		var client = new HttpClient();
		client.BaseAddress = new Uri(AVIATION_STACK_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		return await RequestFromAviationStack(queryString.ToString());
	}
	
	private static async Task<AviationStackFlightPlans> RequestFromAviationStack(string queryString)
	{
		var client = new HttpClient();
		client.BaseAddress = new Uri(AVIATION_STACK_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		HttpResponseMessage response = await client.GetAsync(queryString).ConfigureAwait(false);
		if (response.IsSuccessStatusCode)
		{
			var json = await response.Content.ReadAsStringAsync();
			return AviationStackFlightPlanResponseMapper.DecodeFlightPlansFrom(json);
		}

		return null;
	}
}
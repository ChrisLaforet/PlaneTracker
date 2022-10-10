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
		var response = GetFlightPlansFromAviationStackByFlightICAO(flightICAO).Result;
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
		var response = GetFlightPlansFromAviationStackByFlightIATA(flightIATA).Result;
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

	public List<IFlightPlan> GetFlightPlansForFlightsForAirportICAO(string airportICAO)
	{
		var response = GetFlightPlansForAirportICAOFromAviationStack(airportICAO).Result;
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
	
	public List<IFlightPlan> GetFlightPlansForFlightsForAirportIATA(string airportIATA)
	{
		var response = GetFlightPlansForAirportIATAFromAviationStack(airportIATA).Result;
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
	
	private async Task<AviationStackFlightPlans> GetFlightPlansFromAviationStackByFlightICAO(string flightICAO)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&airline_iata={flightICAO}");
		queryString.Append($"flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		return await RequestFromAviationStack(queryString);
	}
	
	private async Task<AviationStackFlightPlans> GetFlightPlansFromAviationStackByFlightIATA(string flightIATA)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&airline_iata={flightIATA}");
		queryString.Append($"flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		return await RequestFromAviationStack(queryString);
	}

	private async Task<AviationStackFlightPlans> GetFlightPlansForAirportICAOFromAviationStack(string airportICAO)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&dep_icao={airportICAO}");
		queryString.Append($"&arr_icao={airportICAO}");
		queryString.Append($"flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		var client = new HttpClient();
		client.BaseAddress = new Uri(AVIATION_STACK_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		return await RequestFromAviationStack(queryString);
	}
	
	private async Task<AviationStackFlightPlans> GetFlightPlansForAirportIATAFromAviationStack(string airportIATA)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&dep_iata={airportIATA}");
		queryString.Append($"&arr_iata={airportIATA}");
		queryString.Append($"flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
		var client = new HttpClient();
		client.BaseAddress = new Uri(AVIATION_STACK_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		return await RequestFromAviationStack(queryString);
	}
	
	private static async Task<AviationStackFlightPlans> RequestFromAviationStack(StringBuilder queryString)
	{
		var client = new HttpClient();
		client.BaseAddress = new Uri(AVIATION_STACK_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		HttpResponseMessage response = await client.GetAsync(queryString.ToString()).ConfigureAwait(false);
		if (response.IsSuccessStatusCode)
		{
			var json = await response.Content.ReadAsStringAsync();
			return AviationStackFlightPlanResponseMapper.DecodeFlightPlansFrom(json);
		}

		return null;
	}
}
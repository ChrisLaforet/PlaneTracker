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

	public List<IFlightPlan> GetFlightPlanForFlightICAO(string code)
	{
		if (string.IsNullOrEmpty(code))
		{
			return new List<IFlightPlan>();
		}
		var response = GetFlightPlansFromAviationStack(null, code).Result;
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

	public List<IFlightPlan> GetFlightPlanForFlightIATA(string code)
	{
		if (string.IsNullOrEmpty(code))
		{
			return new List<IFlightPlan>();
		}
		var response = GetFlightPlansFromAviationStack(code, null).Result;
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

	private async Task<AviationStackFlightPlans> GetFlightPlansFromAviationStack(string iataCode, string icaoCode)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		if (!string.IsNullOrEmpty(iataCode))
		{
			queryString.Append($"&airline_iata={iataCode}");
		}

		if (!string.IsNullOrEmpty(icaoCode))
		{
			queryString.Append($"&airline_icao={icaoCode}");
		}
		queryString.Append($"flight_date={DateTime.Now.ToString("yyyy-MM-dd")}");
		
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
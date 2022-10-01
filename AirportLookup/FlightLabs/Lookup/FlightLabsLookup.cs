using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using AirportLookup.FlightLabs.Mapper;
using AirportLookup.FlightLabs.Model;

namespace AirportLookup.FlightLabs.Lookup;

public class FlightLabsLookup : IAirportLookup
{
	const string FLIGHT_LABS_URL = "https://app.goflightlabs.com/airports";
	
	private string accessKey;

	public FlightLabsLookup(string accessKey) => this.accessKey = accessKey;
	
	public List<IAirport> FindAirportByICAOCode(string code)
	{
		var response = GetAirportsFromFlightLabs(code).Result;
		var airports = new List<IAirport>();
		foreach (var flightLabsAirport in response)
		{
			if (flightLabsAirport.icao_code.ToLower().Contains(code.ToLower()))
			{
				airports.Add(flightLabsAirport);
			}
		}
		return airports;
	}

	public List<IAirport> FindAirportByIATACode(string code)
	{
		var response = GetAirportsFromFlightLabs(code).Result;
		var airports = new List<IAirport>();
		foreach (var flightLabsAirport in response)
		{
			if (flightLabsAirport.iata_code.ToLower().Contains(code.ToLower()))
			{
				airports.Add(flightLabsAirport);
			}
		}
		return airports;
	}

	private async Task<List<FlightLabsAirport>> GetAirportsFromFlightLabs(string code)
	{
		var queryString = new StringBuilder();
		queryString.Append($"?access_key={this.accessKey}");
		queryString.Append($"&search={code}");

		var client = new HttpClient();
		client.BaseAddress = new Uri(FLIGHT_LABS_URL);
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		HttpResponseMessage response = await client.GetAsync(queryString.ToString()).ConfigureAwait(false);
		if (response.IsSuccessStatusCode)
		{
			var json = await response.Content.ReadAsStringAsync();
			return FlightLabsAirportResponseMapper.DecodeAirportsFrom(json);
		}

		return null;
	}
}
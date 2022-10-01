namespace AirportLookup.FlightLabs.Model;

public class FlightLabsAirport : IAirport
{
	public string gmt { get; set; }
	public string airport_id { get; set; }
	public string iata_code { get; set; }
	public string city_iata_code { get; set; }
	public string icao_code { get; set; }
	public string county_iso2 { get; set; }
	public string geoname_id { get; set; }
	public string latitude { get; set; }
	public string longitude { get; set; }
	public string airport_name { get; set; }
	public string country_name { get; set; }
	public string phone_number { get; set; }
	public string timezone { get; set; }

	public string ICAOCode
	{
		get
		{
			return icao_code;
		}
	}

	public string IATACode
	{
		get
		{
			return iata_code;
		}
	}

	public float AirportLatitude
	{
		get
		{
			return float.Parse(latitude);
		}
	}

	public float AirportLongitude
	{
		get
		{
			return float.Parse(longitude);
		}
	}

	public string Name
	{
		get
		{
			return airport_name;
		}
	}

	public string Country
	{
		get
		{
			return country_name;
		}
	}

	public string CountryCode
	{
		get
		{
			return county_iso2;
		}
	}

	public string AirportTimezone
	{
		get
		{
			return timezone;
		}
	}
}
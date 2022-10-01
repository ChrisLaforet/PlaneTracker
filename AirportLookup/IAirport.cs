namespace AirportLookup;

public interface IAirport
{
	string ICAOCode { get; }
	string IATACode { get; }
	float AirportLatitude { get; }
	float AirportLongitude { get; }
	string Name { get;  }
	string Country { get; }
	string CountryCode { get; }
	string AirportTimezone { get; }
}
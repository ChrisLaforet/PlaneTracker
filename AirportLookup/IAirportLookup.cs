namespace AirportLookup;

public interface IAirportLookup
{
	public List<IAirport> FindAirportByICAOCode(string code);
	public List<IAirport> FindAirportByIATACode(string code);
}
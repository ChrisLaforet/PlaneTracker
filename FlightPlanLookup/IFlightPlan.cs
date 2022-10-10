namespace FlightPlanLookup;

public interface IFlightPlan
{
	public string DepartureAirportName { get; }
	public string DepartureAirportIATACode { get; }
	public string DepartureAirportICAOCode { get; }
	public string ArrivalAirportName { get; }
	public string ArrivalAirportIATACode { get; }
	public string ArrivalAirportICAOCode { get; }
	public string AircraftRegistration { get; }
	public string AirlineName { get; }
	public string AirlineCodeIATA { get; }
	public string AirlineCodeICAO { get; }
	public string FlightNumberIATA { get; }
	public string FlightNumberICAO { get; }
	public string AircraftICAO24 { get; }
	public string AircraftTypeIATA { get; }
	public string AircraftTypeICAO { get; }
}
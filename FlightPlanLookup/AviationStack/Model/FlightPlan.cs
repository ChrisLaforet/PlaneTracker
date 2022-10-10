namespace FlightPlanLookup.AviationStack.Model;

public class FlightPlan : IFlightPlan
{
	public string DepartureAirportName { get; set; }
	public string DepartureAirportIATACode { get; set; }
	public string DepartureAirportICAOCode { get; set; }
	public string ArrivalAirportName { get; set; }
	public string ArrivalAirportIATACode { get; set; }
	public string ArrivalAirportICAOCode { get; set; }
	public string? AircraftRegistration { get; set; }
	public string? AirlineName { get; set;  }
	public string? AirlineCodeIATA { get; set; }
	public string? AirlineCodeICAO { get; set; }
	public string? FlightNumberIATA { get; set; }
	public string? FlightNumberICAO { get; set; }
	public string? AircraftICAO24 { get; set; }
	public string? AircraftTypeIATA { get; set; }
	public string? AircraftTypeICAO { get; set; }
}
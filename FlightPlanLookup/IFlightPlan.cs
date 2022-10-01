namespace FlightPlanLookup;

public interface IFlightPlan
{
	public string DepartureAirportName { get; }
	public string DepartureAirportIATACode { get; }
	public string DepartureAirportICAOCode { get; }
	public string ArrivalAirportName { get; }
	public string ArrivalAirportIATACode { get; }
	public string ArrivalAirportICAOCode { get; }
}
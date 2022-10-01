namespace FlightPlanLookup.AviationStack.Model;

public class FlightPlan : IFlightPlan
{
	public string DepartureAirportName { get; set; }
	public string DepartureAirportIATACode { get; set; }
	public string DepartureAirportICAOCode { get; set; }
	public string ArrivalAirportName { get; set; }
	public string ArrivalAirportIATACode { get; set; }
	public string ArrivalAirportICAOCode { get; set; }
}
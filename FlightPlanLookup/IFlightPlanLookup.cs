namespace FlightPlanLookup;

public interface IFlightPlanLookup
{
	public List<IFlightPlan> GetFlightPlansForFlightsForAirportICAO(string airportICAO);
	public List<IFlightPlan> GetFlightPlansForFlightsForAirportIATA(string airportIATA);

	public List<IFlightPlan> GetFlightPlanForFlightICAO(string flightICAO);
	public List<IFlightPlan> GetFlightPlanForFlightIATA(string flightIATA);
}
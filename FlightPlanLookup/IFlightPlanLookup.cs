namespace FlightPlanLookup;

public interface IFlightPlanLookup
{
	public List<IFlightPlan> GetFlightPlanForFlightICAO(string code);
	public List<IFlightPlan> GetFlightPlanForFlightIATA(string code);
}
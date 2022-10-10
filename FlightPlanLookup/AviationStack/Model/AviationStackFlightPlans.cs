namespace FlightPlanLookup.AviationStack.Model;

public class AviationStackFlightPlans
{
    public Pagination pagination { get; set; }
    public List<Plan> data { get; set; }

    public List<Plan> plans
    {
        get
        {
            return data;
        }
    }
}

public class Plan
{
    public string flight_date { get; set; }
    public string flight_status { get; set; }
    public Departure departure { get; set; }
    public Arrival arrival { get; set; }
    public Airline airline { get; set; }
    public Flight flight { get; set; }
    public Aircraft aircraft { get; set; }
    public Live live { get; set; }

    public FlightPlan FlightPlan
    {
        get
        {
            return new FlightPlan()
            {
                ArrivalAirportName = arrival.airport,
                ArrivalAirportIATACode = arrival.iata,
                ArrivalAirportICAOCode = arrival.icao,
                DepartureAirportName = departure.airport,
                DepartureAirportIATACode = departure.iata,
                DepartureAirportICAOCode = departure.icao,
                AircraftRegistration = aircraft.registration,
                AirlineName = airline.name,
                AirlineCodeIATA = airline.iata,
                AirlineCodeICAO = airline.icao,
                FlightNumberIATA = flight.iata,
                FlightNumberICAO = flight.icao,
                AircraftICAO24 = aircraft.icao24,
                AircraftTypeIATA = aircraft.iata,
                AircraftTypeICAO = aircraft.icao
            };

        }
    }
}

public class Aircraft
{
    public string registration { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
    public string icao24 { get; set; }
}

public class Airline
{
    public string name { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
}

public class Arrival
{
    public string airport { get; set; }
    public string timezone { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
    public string terminal { get; set; }
    public string gate { get; set; }
    public string baggage { get; set; }
    public int delay { get; set; }
    public DateTime scheduled { get; set; }
    public DateTime estimated { get; set; }
    public object actual { get; set; }
    public object estimated_runway { get; set; }
    public object actual_runway { get; set; }
}

public class Departure
{
    public string airport { get; set; }
    public string timezone { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
    public string terminal { get; set; }
    public string gate { get; set; }
    public int delay { get; set; }
    public DateTime scheduled { get; set; }
    public DateTime estimated { get; set; }
    public DateTime actual { get; set; }
    public DateTime estimated_runway { get; set; }
    public DateTime actual_runway { get; set; }
}

public class Flight
{
    public string number { get; set; }
    public string iata { get; set; }
    public string icao { get; set; }
    public object codeshared { get; set; }
}

public class Live
{
    public DateTime updated { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double altitude { get; set; }
    public double direction { get; set; }
    public double speed_horizontal { get; set; }
    public double speed_vertical { get; set; }
    public bool is_ground { get; set; }
}

public class Pagination
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int count { get; set; }
    public int total { get; set; }
}


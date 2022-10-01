using System.Diagnostics.Contracts;
using ADSBLookup;
using ADSBLookup.OpenNetwork.Lookup;
using AirportLookup.FlightLabs.Lookup;
using Configurator;
using Coordinator;
using FlightPlanLookup;
using FlightPlanLookup.AviationStack.Lookup;

namespace PlaneTrackerConsole;

public class PlaneTrackerConsole
{
    private const string PATH_TO_CONFIG = "c:/dropbox/xmlconfiguration/planetracker.xml";       // set this to your path - it could be local or shared (as in this example)
    private const string AVIATION_STACK_KEY = "AviationStackKey";
    private const string FLIGHT_LABS_KEY = "FlightLabsKey";
    
    private static IPlaneLookup lookup = new OpenNetworkLookup();
    
    public static void Main(string[] args)
    {
        var iataCode = "RDU";       // default is Raleigh Durham
        if (args.Length > 0)
        {
            iataCode = args[0];
        }
        
        var configurationLoader = new XmlConfigurationLoader(PATH_TO_CONFIG);

        var airportLookup = new FlightLabsLookup(configurationLoader.GetKeyValueFor(FLIGHT_LABS_KEY));
        Console.WriteLine($"Searching for airport with IATA code of {iataCode}");
        var matches = airportLookup.FindAirportByIATACode(iataCode);
        if (matches.Count != 1)
        {
            Console.WriteLine($"Unable to find a single entry for IATA code of {iataCode}");
            return;
        }

        var airport = matches[0];
        Console.WriteLine($"Loaded data for airport {airport.Name} with IATA code of {iataCode}");
        
        var flightPlanLookup = new AviationStackLookup(configurationLoader.GetKeyValueFor(AVIATION_STACK_KEY));

        // Testing airport coordinates with a box extending 50NM each compass cardinal direction (NSEW)
//      var planes = lookup.GetPlanesCenteredOn(new Coordinate(35.8801f), new Coordinate(-78.7880f), 100f);
        var planes = lookup.GetPlanesCenteredOn(new Coordinate(airport.AirportLatitude), new Coordinate(airport.AirportLongitude), 100f);
        foreach (var plane in planes)
        {
            ShowPlane(plane, flightPlanLookup);
        }
    }

    private static void ShowPlane(IPlane plane, IFlightPlanLookup flightPlanLookup)
    {
        Console.WriteLine($"{plane.ICAO24} -> Call sign: {plane.CallSign} -- Type: {plane.PlaneType}");
        Console.WriteLine($"\tPosition: {plane.Latitude} (lat), {plane.Longitude} (long)");
        Console.WriteLine($"\tTrack: {plane.TrueTrack}");
        Console.WriteLine($"\tAltitude (ft): {plane.AltitudeInFeet}");
        Console.WriteLine($"\tSpeed (kt): {plane.VelocityInKnots}");
        Console.WriteLine($"\tVertical rate (ft/s): {plane.VerticalRateInFeetPerSecond}");

        // if (plane.PlaneType == PlaneType.Plane && plane.CallSign != null && plane.CallSign.Length == 7)
        // {
        //     var flightPlans = flightPlanLookup.GetFlightPlanForFlightIATA(plane.CallSign);
        //     if (flightPlans.Count > 0)
        //     {
        //         var flightPlan = flightPlans[0];
        //         Console.WriteLine($"$\tFlight departed: {flightPlan.DepartureAirportName} -- ICAO: {flightPlan.DepartureAirportICAOCode} -- IATA: {flightPlan.DepartureAirportIATACode}");
        //         Console.WriteLine($"$\tFlight destination: {flightPlan.ArrivalAirportName} -- ICAO: {flightPlan.ArrivalAirportICAOCode} -- IATA: {flightPlan.ArrivalAirportIATACode}");
        //     }
        // }

        Console.WriteLine("-------------");
        Console.WriteLine();
    }
}
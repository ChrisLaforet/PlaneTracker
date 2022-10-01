﻿using ADSBLookup;
using ADSBLookup.OpenNetwork.Lookup;
using Coordinator;

namespace PlaneTrackerConsole;

public class PlaneTrackerConsole
{
    private static IPlaneLookup lookup = new OpenNetworkLookup();
    
    public static void Main(string[] args)
    {
        var planes = lookup.GetPlanesCenteredOn(new Coordinate(35.4f), new Coordinate(-80f), 250f);
        foreach (var plane in planes)
        {
            Console.Write(plane.ICAO24);
            Console.Write("->");
            Console.Write(plane.CallSign);
            Console.Write(": Altitude (ft): ");
            Console.Write(plane.AltitudeInFeet);
            Console.Write(": Speed (kt): ");
            Console.Write(plane.VelocityInKnots);
            Console.WriteLine();
        }
    }
}
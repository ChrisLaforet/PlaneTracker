# PlaneTracker
A learning TDD project for tracking planes with adsb

The purpose of this development effort is to wire ADSB aircraft lookups through one (or more) providers. Initially it will use opensky-network's free api.  An attempt will also be made to derive flight-plan data from one (or more) providers.

The project permits exploration of latitude and longitude issues, determining what aircraft are doing, and other "fun" activities.  

This code is not intended for any critical use that can potentially result in injury or death or violation of laws in force for a specific domain.  In other words, please don't use this code as-is to build any mission-critical applications!

The issue with AviationStack is there is no way to comprehensively build a database of flight plans with only 100 free lookups a month (limited to 100 results at a time).  So the approach would be to just look at flights originating and terminating at the airport being checked in the console app.

Links to provider apis used in this code:

OpenSky Network (ADSB data): https://openskynetwork.github.io/opensky-api/rest.html

AviationStack (Flight plans/100 free per month): https://aviationstack.com/documentation

FlightLabs (Airport lookups/100 free per month): https://app.goflightlabs.com/dashboard#getAirportData

Amadeus (Airport lookups): https://developers.amadeus.com/self-service/category/air/api-doc/airport-and-city-search/api-reference

Amadeus (OAuth support): https://developers.amadeus.com/self-service/apis-docs/guides/authorization-262


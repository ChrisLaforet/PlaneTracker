# PlaneTracker
A learning TDD project for tracking planes with adsb

The purpose of this development effort is to wire ADSB aircraft lookups through one (or more) providers. Initially it will use opensky-network's free api.  An attempt will also be made to derive flight-plan data from one (or more) providers.

The project permits exploration of latitude and longitude issues, determining what aircraft are doing, and other "fun" activities.  

This code is not intended for any critical use that can potentially result in injury or death or violation of laws in force for a specific domain.  In other words, please don't use this code as-is to build any mission-critical applications!

Links to provider apis used in this code:

OpenSky Network (ADSB data): https://openskynetwork.github.io/opensky-api/rest.html

AviationStack (Flight plans): https://aviationstack.com/documentation

Amadeus (Airport lookups): https://developers.amadeus.com/self-service/category/air/api-doc/airport-and-city-search/api-reference

Amadeus (OAuth support): https://developers.amadeus.com/self-service/apis-docs/guides/authorization-262



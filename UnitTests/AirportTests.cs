using AirportLookup.FlightLabs.Mapper;

namespace UnitTests;

public class AirportTests
{
	public const string SINGLE_RESPONSE = "[{\"id\":\"1875\",\"gmt\":\"3\",\"airport_id\":\"1875\",\"iata_code\":\"DOH\",\"city_iata_code\":\"DOH\",\"icao_code\":\"OTBD\",\"country_iso2\":\"QA\",\"geoname_id\":\"6300133\",\"latitude\":\"25.267569\",\"longitude\":\"51.558067\",\"airport_name\":\"Doha International\",\"country_name\":\"Qatar\",\"phone_number\":\"+974 4465 666\",\"timezone\":\"Asia/Qatar\"}]";

	public const string DOUBLE_RESPONSE =
		"[{\"id\":\"2752\",\"gmt\":\"-5\",\"airport_id\":\"2752\",\"iata_code\":\"GSO\",\"city_iata_code\":\"GSO\",\"icao_code\":\"KGSO\",\"country_iso2\":\"US\",\"geoname_id\":\"4469156\",\"latitude\":\"36.105324\",\"longitude\":\"-79.9373\",\"airport_name\":\"Piedmont Triad International\",\"country_name\":\"United States\",\"phone_number\":\"336-665-5666\",\"timezone\":\"America/New_York\"},{\"id\":\"3628\",\"gmt\":\"2\",\"airport_id\":\"3628\",\"iata_code\":\"JSY\",\"city_iata_code\":\"JSY\",\"icao_code\":\"LGSO\",\"country_iso2\":\"GR\",\"geoname_id\":\"6299510\",\"latitude\":\"37.42361\",\"longitude\":\"24.95\",\"airport_name\":\"Syros Island\",\"country_name\":\"Greece\",\"phone_number\":null,\"timezone\":\"Europe/Athens\"}]";
	
	[Fact]
	public void GivenSingleResponseFromFlightLabs_WhenParsed_ThenReturnsSingleAirportData()
	{
		var airports = FlightLabsAirportResponseMapper.DecodeAirportsFrom(SINGLE_RESPONSE);
		Assert.Equal(1, airports.Count);
		Assert.Equal("OTBD", airports[0].ICAOCode);
		Assert.Equal("DOH", airports[0].IATACode);
		Assert.Equal(25.267569f, airports[0].AirportLatitude);
		Assert.Equal(51.558067f, airports[0].AirportLongitude);
		Assert.Equal("Doha International", airports[0].Name);
	}
	
	[Fact]
	public void GivenDoubleResponseFromFlightLabs_WhenParsed_ThenReturnsTwoAirportsData()
	{
		var airports = FlightLabsAirportResponseMapper.DecodeAirportsFrom(DOUBLE_RESPONSE);
		Assert.Equal(2, airports.Count);
		Assert.Equal("GSO", airports[0].IATACode);
		Assert.Equal(36.105324f, airports[0].AirportLatitude);
		Assert.Equal(-79.9373f, airports[0].AirportLongitude);
		Assert.Equal("Piedmont Triad International", airports[0].Name);
		
		Assert.Equal("JSY", airports[1].IATACode);
		Assert.Equal(37.42361f, airports[1].AirportLatitude);
		Assert.Equal(24.95f, airports[1].AirportLongitude);
		Assert.Equal("Syros Island", airports[1].Name);
	}
}
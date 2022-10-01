using System.Xml.Linq;

namespace Configurator;

public class XmlConfigurationLoader : IConfigurationLoader
{
	private readonly string pathToConfiguration;

	public XmlConfigurationLoader(string pathToConfiguration) => this.pathToConfiguration = pathToConfiguration;

	public string GetKeyValueFor(string elementName)
	{
		var fileElements = XElement.Load(pathToConfiguration);
		var element = fileElements.Element(elementName);
		if (element != null)
		{
			return element.Value;
		}
		throw new KeyNotFoundException();
	}
	// sample usage:
	// IConfigurationLoader configurationLoader = new XmlConfigurationLoader(PATHNAME_TO_CONFIGURATION_FILE);
	//
	// string dbConnection = configurationLoader.GetKeyValueFor("DatabaseConnection");
	//
	// format of config file:
	// <?xml version="1.0" encoding="utf-8" standalone="yes"?>
	// <Values>
	// 	<DatabaseConnection>Data Source=localhost;MultipleActiveResultSets=true;Initial Catalog=test;User Id=username;Password=password</DatabaseConnection>
	// </Values>
	
}
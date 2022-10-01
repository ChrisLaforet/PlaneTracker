namespace Configurator;

public interface IConfigurationLoader
{
	string GetKeyValueFor(string elementName);
}
namespace roguelike.roguelike.utils.resources.configuring.configs;

public class ConfigNetworking : Config
{
  public ConfigNetworking()
  {
    Address = "127.0.0.1";
    Port = 5000;
  }

  public string Address { get; private set; }
  public int Port { get; private set; }

  public override string GetName()
  {
    return "networking.json";
  }

  public ConfigNetworking SetAddress(string value)
  {
    Address = value;
    return this;
  }

  public ConfigNetworking SetPort(int value)
  {
    Port = value;
    return this;
  }
}
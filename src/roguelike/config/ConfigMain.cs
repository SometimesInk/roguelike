namespace roguelike.roguelike.config;

public class ConfigMain : Config
{
  public ConfigMain()
  {
    ActiveLocale = "en_US.lang";
  }

  public string ActiveLocale { get; private set; }

  public override string GetName()
  {
    return "main.json";
  }

  public ConfigMain SetActiveLocale(string value)
  {
    ActiveLocale = value;
    return this;
  }
}
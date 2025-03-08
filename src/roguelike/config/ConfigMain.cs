namespace roguelike.roguelike.config;

public class ConfigMain(string activeLocale = "en_US.lang") : Config
{
  public string ActiveLocale { get; private set; } = activeLocale;

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
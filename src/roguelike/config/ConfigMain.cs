namespace roguelike.roguelike.config;

public class ConfigMain : Config
{
  public string ActiveLocale = "en_US.lang";

  public override string GetName()
  {
    return "main.json";
  }
}
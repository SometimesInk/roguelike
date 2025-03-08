using roguelike.roguelike.config;

namespace roguelike.roguelike.engine.handle;

public static class HandlerConfig
{
  private static readonly List<Config> RegisteredConfigs = [];

  public static void Init()
  {
    // Register configs
    RegisteredConfigs.Add(Config.Read<ConfigMain>("main.json"));
  }

  // TODO: Make a method to easily write to config (just like how GetConfig<T>()
  //  makes reading config easier, thus using types maybe perhaps?)

  public static T GetConfig<T>()
  {
    foreach (Config conf in RegisteredConfigs.Where(conf => conf.GetType() == typeof(T)))
    {
      return (T)Convert.ChangeType(conf, typeof(T));
    }

    // TODO: Safe mode for this
    throw new ArgumentException();
  }
}
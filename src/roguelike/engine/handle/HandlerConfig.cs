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

  public static T GetConfig<T>()
  {
    foreach (Config conf in RegisteredConfigs.Where(conf => conf.GetType() == typeof(T)))
    {
      // TODO: Safe mode for this convert
      return (T)Convert.ChangeType(conf, typeof(T));
    }

    // TODO: Safe mode for this
    throw new ArgumentException();
  }
}
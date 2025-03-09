using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.config;

public static class HandlerConfig
{
  private static readonly List<Config> RegisteredConfigs = [];

  public static void Init()
  {
    // Register configs
    RegisteredConfigs.Add(Config.Read<ConfigMain>("main.json"));
    RegisteredConfigs.Add(Config.Read<ConfigNetworking>("networking.json"));
  }

  /// <summary>
  /// Returns the found registered config under the given type T. If there are
  ///  multiple registered config of said type, the first one found should be
  ///  returned, although this case should not happen ever if the registered
  ///  configs list is maintained properly.
  /// </summary>
  /// <typeparam name="T">Type of the config to find.</typeparam>
  /// <returns>The config of the given type T.</returns>
  /// <exception cref="ArgumentException">If the entered type T does not extend
  ///  Config, or if no config of said type was found in the registered configs
  ///  list.</exception>
  public static T GetConfig<T>()
  {
    // Check if type param is of the correct instance
    if (typeof(T).BaseType != typeof(Config)) throw new ArgumentException("Type param does not extend Config.");

    foreach (Config conf in RegisteredConfigs.Where(conf => conf.GetType() == typeof(T)))
    {
      // This conversion should always be successful, as the type T is checked
      //  at the start of this method.
      return (T)Convert.ChangeType(conf, typeof(T));
    }

    Translatable.Printf("config.error.missingConfig", stream: TranslatablePrintStream.Err,
      form: typeof(T).ToString());
    throw new ArgumentException($"Missing configuration '{typeof(T)}'.");
  }
}
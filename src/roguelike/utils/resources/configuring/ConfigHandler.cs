using Newtonsoft.Json;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.configuring.configs;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.utils.resources.configuring;

public static class ConfigHandler
{
  private static readonly List<Config> RegisteredConfigs = [];

  /// <summary>
  /// Initializes config files.
  /// </summary>
  /// <param name="win">Window for logging.</param>
  /// <remarks>Requires no previous initialization.</remarks>
  public static void Init(EntryWindow win)
  {
    // Register configs
    RegisteredConfigs.Add(Read<ConfigMain>(win, "main.json"));
    RegisteredConfigs.Add(Read<ConfigNetworking>(win, "networking.json"));
  }

  /// <summary>
  /// Returns the found registered config under the given type T. If there are
  ///  multiple registered config of said type, the first one found should be
  ///  returned, although this case should not happen ever if the registered
  ///  configs list is maintained properly.
  /// </summary>
  /// <param name="win">Window for logging.</param>
  /// <typeparam name="T">Type of the config to find.</typeparam>
  /// <returns>The config of the given type T.</returns>
  /// <exception cref="ArgumentException">If the entered type T does not extend
  ///  Config, or if no config of said type was found in the registered configs
  ///  list.</exception>
  public static T GetConfig<T>(EntryWindow win)
  {
    // Check if type param is of the correct instance
    if (typeof(T).BaseType != typeof(Config)) throw new ArgumentException("Type param does not extend Config.");

    // Find and return desired config
    foreach (Config conf in RegisteredConfigs.Where(conf => conf.GetType() == typeof(T)))
    {
      // This conversion should always be successful, as the type T is checked
      //  at the start of this method.
      return (T)Convert.ChangeType(conf, typeof(T));
    }

    // Log error, no desired config found
    win.WriteMessage(new Translatable("config.error.missingConfig").Format(typeof(T).ToString()),
      TranslatablePrintStream.Err);
    throw new ArgumentException($"Missing configuration '{typeof(T)}'.");
  }

  /// <summary>
  /// Deserialize the given file.
  /// </summary>
  /// <param name="name">Name of the config file.</param>
  /// <param name="win">Window for logging.</param>
  /// <typeparam name="T">Type to deserialize as.</typeparam>
  /// <returns>The deserialized config object, or its default state if safe mode
  ///  is on and an exception occurred.</returns>
  /// <exception cref="ArgumentException">Type param is not a config type.
  /// </exception>
  /// <exception cref="FileNotFoundException">Config file does not exist.
  /// </exception>
  /// <exception cref="InvalidOperationException">Could not deserialize.
  /// </exception>
  /// <remarks>Exceptions are caught when safe mode is activated.</remarks>
  private static T Read<T>(EntryWindow win, string name) where T : new()
  {
    // Check if type param is of the correct instance.
    if (typeof(T).BaseType != typeof(Config)) throw new ArgumentException("Type param does not extend Config.");

    try
    {
      string contents = Resources.ReadPath("conf", name);
      return JsonConvert.DeserializeObject<T>(contents) ?? throw new InvalidOperationException();
    }
    catch (FileNotFoundException)
    {
      win.WriteMessage(new Translatable("config.error.missingConfig").Format(name),
        TranslatablePrintStream.Err);

#if SAFE_MODE
      return new T();
#else
      throw;
#endif
    }
    catch (InvalidOperationException)
    {
      win.WriteMessage(new Translatable("config.error.notDeserializable").Format(name),
        TranslatablePrintStream.Err);

#if SAFE_MODE
      return new T();
#else
      throw;
#endif
    }
    catch (Exception)
    {
      // Potential Memory/I.O error
      win.WriteMessage(new Translatable("config.error").Format(name), TranslatablePrintStream.Err);
#if SAFE_MODE
      return new T();
#else
      throw;
#endif
    }
  }
}
// In this class, SAFE MODE does the following:
//  - Makes the Read<T>(string) method return the default config on failure
//    instead of throwing an error.

// #define SAFE_MODE

using Newtonsoft.Json;
using roguelike.roguelike.util.resources;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.config;

public abstract class Config
{
  public abstract string GetName();

  /// <summary>
  /// Deserialize the given file.
  /// </summary>
  /// <param name="name">Name of the config file.</param>
  /// <typeparam name="T">Type to deserialize as.</typeparam>
  /// <returns>The deserialized config object, or its default state if safe mode
  ///  is on and an exception occurred.</returns>
  /// <exception cref="ArgumentException">Type param is not a config type.
  /// </exception>
  /// <exception cref="FileNotFoundException">Config file does not exist.
  /// </exception>
  /// <exception cref="InvalidOperationException">Could not deserialize.
  /// </exception>
  public static T Read<T>(string name) where T : new()
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
      Translatable.Printf("config.error.missingConfig", stream: TranslatablePrintStream.Err, form: name);

#if SAFE_MODE
      return new T();
#else
      throw;
#endif
    }
    catch (InvalidOperationException)
    {
      Translatable.Printf("config.error.notDeserializable", stream: TranslatablePrintStream.Err, form: name);

#if SAFE_MODE
      return new T();
#else
      throw;
#endif
    }
  }

  public void Write()
  {
    string contents = JsonConvert.SerializeObject(this, GetType(), Formatting.Indented, new JsonSerializerSettings());
    File.WriteAllText(ToString(), contents);
  }

  /// <summary>
  /// Returns the path of the config file.
  /// </summary>
  /// <returns>Path to the config file.</returns>
  public override string ToString()
  {
    // Return path
    return Resources.GetResourcePath("conf", GetName());
  }
}
// In this class, SAFE MODE does the following:
//  - Makes the Read<T>(string) method return the default config on failure
//    instead of throwing an error.

// #define SAFE_MODE

using Newtonsoft.Json;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.utils.resources.configuring;

public abstract class Config
{
  public abstract string GetName();

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
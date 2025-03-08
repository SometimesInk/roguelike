using Newtonsoft.Json;
using roguelike.roguelike.util.resources;

namespace roguelike.roguelike.config;

public abstract class Config
{
  public abstract string GetName();

  public static T Read<T>(string name)
  {
    return JsonConvert.DeserializeObject<T>(Resources.ReadPath(Resources.GetResourcePath(Path.Combine("conf",
      name)))) ?? throw new InvalidOperationException();
  }

  // TODO: Fix writing
  public static void Write(Config conf)
  {
    File.WriteAllText(conf.ToString(), JsonConvert.SerializeObject(conf));
  }

  public override string ToString()
  {
    // Return path
    return Resources.GetResourcePath(Path.Combine("conf", GetName()));
  }
}
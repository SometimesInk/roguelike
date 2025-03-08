using Newtonsoft.Json;
using roguelike.roguelike.util.resources;

namespace roguelike.roguelike.config;

public abstract class Config
{
  public abstract string GetName();

  public static T Read<T>(string name)
  {
    // TODO: check for contents' definition and safe mode for this
    string contents = Resources.ReadPath("conf", name);
    return JsonConvert.DeserializeObject<T>(contents) ?? throw new InvalidOperationException();
  }

  public void Write<T>()
  {
    // TODO: Safe mode for this, and perhaps a way to easily convert from type
    //  T1 to T2 as this piece of code is often repeated.
    T conf = (T)Convert.ChangeType(this, typeof(T));

    string contents = JsonConvert.SerializeObject(conf, typeof(T), Formatting.Indented, new JsonSerializerSettings());

    // TODO: Safe mode here to check cast
    File.WriteAllText(conf.ToString(), contents);
  }

  public override string ToString()
  {
    // Return path
    return Resources.GetResourcePath("conf", GetName());
  }
}
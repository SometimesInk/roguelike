using System.Text;

namespace roguelike.roguelike.util.resources;

public static class Resources
{
  private static readonly string AssetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "resources",
    "assets");

  public static string GetResourcePath(params string[] steps)
  {
    return Path.Combine(AssetsPath, Path.Combine(steps));
  }

  public static string ReadPath(string path)
  {
    // Read file path using StreamReader
    using StreamReader streamReader = new(path, Encoding.UTF8);
    string readContents = streamReader.ReadToEnd();
    return readContents;
  }

  public static string ReadPath(params string[] steps)
  {
    return ReadPath(GetResourcePath(steps));
  }
}
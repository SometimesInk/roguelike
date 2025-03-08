using System.Text;

namespace roguelike.roguelike.util.resources;

public static class Resources
{
  private static readonly string
    AssetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "resources", "assets");

  public static string GetResourcePath(string asset)
  {
    return Path.Combine(AssetsPath, asset);
  }

  public static string ReadPath(string path)
  {
    // Read file path using StreamReader
    using StreamReader streamReader = new(path, Encoding.UTF8);
    string readContents = streamReader.ReadToEnd();
    return readContents;
  }
}
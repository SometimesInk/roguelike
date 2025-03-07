namespace roguelike.roguelike.util.resources;

public static class Resources
{
  private static readonly string
    AssetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "resources", "assets");

  public static string GetResourcePath(string asset)
  {
    return Path.Combine(AssetsPath, asset);
  }
}
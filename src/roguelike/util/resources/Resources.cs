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

  /// <summary>
  /// Reads the file at the given path.
  /// </summary>
  /// <param name="steps">The path steps to take from the resource directory to
  ///  the file's directory.</param>
  /// <returns>Contents of the file.</returns>
  /// <exception cref="FileNotFoundException">If the file does not exist.</exception>
  public static string ReadPath(params string[] steps)
  {
    string path = GetResourcePath(steps);

    // Check if file exists
    if (!File.Exists(path)) throw new FileNotFoundException();

    // Read file
    using StreamReader streamReader = new(path, Encoding.UTF8);
    string readContents = streamReader.ReadToEnd();
    return readContents;
  }
}
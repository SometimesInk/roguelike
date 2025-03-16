// In this class, SAFE MODE does the following:

// #define SAFE_MODE

// When VERBOSE is not defined, the Log() method will not do anything. Thanks in
//  advance.

#define VERBOSE

using roguelike.roguelike.utils.resources.locale;

namespace roguelike.roguelike.utils.resources.translatables;

public record Translatable
{
  private object[]? formatElements;
  public readonly string Key;

  public static readonly Translatable Empty = new("translatable.empty");
  public static readonly Translatable Debug = new("translatable.debug");

  public Translatable(string key)
  {
    Key = key;
  }

  #region Printing

  public static void Log(string message)
  {
#if VERBOSE
    Console.WriteLine($"[INFO] {message}");
#endif
  }

  /// <summary>
  /// This method uses <c>string.Format(...)</c> to format the translatable with
  ///  the given arguments.
  /// </summary>
  /// <param name="formatElements">The objects to parse the translatable with.
  /// </param>
  /// <returns>The formatted string, or its unformatted state if there was an
  /// error and safe mode is defined.</returns>
  /// <exception cref="ArgumentException">If safe mode is undefined, from
  /// string.Format().</exception>
  /// <exception cref="FormatException">If safe mode is undefined, from
  /// string.Format().</exception>
  public Translatable Format(params object[] o)
  {
    formatElements = o;
    return this;
  }

  #endregion

  /// <summary>
  /// Get the formatted value of the translatable key.
  /// </summary>
  /// <returns>The formatted value of the translatable key.</returns>
  public override string ToString()
  {
    // Get value from language locale file
    string value = LocaleHandler.GetTranslatableValue(this);

    // Check if the value requires formatting
    if (formatElements is null) return value;

    // Format the value based on given elements
    string formattedString;
#if SAFE_MODE
    try
    {
#endif
    formattedString = string.Format(value, formatElements);
#if SAFE_MODE
    }
    catch (Exception)
    {
      return ToString();
    }
#endif
    return formattedString;
  }
}
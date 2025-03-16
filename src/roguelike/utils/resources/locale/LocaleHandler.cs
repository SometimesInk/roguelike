// In this class, SAFE MODE does the following:
//  - It makes the GetLangValue(...) not throw an exception on failure, but
//    instead return the translatable's key.

// #define SAFE_MODE

// In this class, CHECKS does the following:
//  - Prints the number of translatables found for manual checking;
//  - Checks if the Translatable.Empty key is defined or not (it should not be);
//  - Checks if the Translatable.Debug key exists and is '{0}' (as it should).
//  - Checks if the selected locale has any missing keys

#define CHECKS

using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.configuring;
using roguelike.roguelike.utils.resources.configuring.configs;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.utils.resources.locale;

public static class LocaleHandler
{
  public static string ActiveLocale { get; private set; } = "en_US.lang";

  private static Dictionary<string, string> cachedLocale = [];

  /// <summary>
  /// Initialize locale.
  /// </summary>
  /// <param name="win">Window for logging.</param>
  /// <exception cref="NullReferenceException">If the cache could not be
  ///  generated.</exception>
  /// <remarks>Requires ConfigHandler to be defined previously.</remarks>
  public static void Init(EntryWindow win)
  {
    // Set active locale to given config value
    ActiveLocale = ConfigHandler.GetConfig<ConfigMain>(win).ActiveLocale;

    // Cache
    cachedLocale = GenerateCache(ActiveLocale);
    if (cachedLocale == null)
    {
      throw new NullReferenceException();
    }

#if CHECKS
    // Check if cache was correctly generated based on the main locale en_US
    if (ActiveLocale != "en_US.lang")
    {
      foreach (var key in GenerateCache("en_US.lang").Keys
                 .Where(key => !cachedLocale.ContainsKey(key)))
      {
        win.WriteMessage(new Translatable("translatable.error.missing").Format(key),
          TranslatablePrintStream.Err);
      }
    }

    win.WriteMessage(new Translatable("translatable.init.checks").Format(cachedLocale.Count),
      TranslatablePrintStream.Info);

    // Check if Translatable.Empty is correctly undefined and if
    //  Translatable.Debug is correctly defined as '{0}'.
    try
    {
      if (Translatable.Empty.ToString() != Translatable.Empty.Key)
        win.WriteMessage(new Translatable("translatable.error.definedEmpty"), TranslatablePrintStream.Err);
    }
    catch (KeyNotFoundException)
    {
      // Good.
    }

    if (Translatable.Debug.ToString() != "{0}")
      win.WriteMessage(new Translatable("translatable.error.abnormalDebug"), TranslatablePrintStream.Err);
#endif
  }

  private static Dictionary<string, string> GenerateCache(string name)
  {
    Dictionary<string, string> output = [];

    // Add all non-null-or-whitespace lines to the cache
    foreach (string line in File.ReadLines(Resources.GetResourcePath("lang", name)))
    {
      string trimmedLine = line.Trim();

      // Remove empty lines and comments
      if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith(';')) continue;

      // Add line to dictionary
      string[] kvp = line.Split(['='], 2);
      output.Add(kvp[0].Trim().ToLower(), kvp[1].Trim());
    }

    return output;
  }

  /// <summary>
  /// Gets the value of a translatable key.
  /// </summary>
  /// <param name="translatable">Translatable to read the key from.</param>
  /// <returns>The key's value on success, or its key on failure with safe mode
  ///  activated.</returns>
  /// <exception cref="KeyNotFoundException">On failure without safe mode
  ///  activated.</exception>
  public static string GetTranslatableValue(Translatable translatable)
  {
    // Read translatable's value from cache
    if (cachedLocale.TryGetValue(translatable.Key.ToLower(), out string? value)) return value;
#if SAFE_MODE
    return translatable.Key;
#else
    throw new KeyNotFoundException($"Could not find a translation key for '{translatable.Key}'.");
#endif
  }
}
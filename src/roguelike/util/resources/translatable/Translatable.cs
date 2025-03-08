// In this class, SAFE MODE does the following:
//  - It makes the GetLangValue(...) not throw an exception on failure, but
//    instead return the translatable's key.
//  - It makes the Format(...) method not throw an exception on failure, but
//    instead returns the unformatted value.

// #define SAFE_MODE

// When VERBOSE is not defined, the Log() method will not do anything. Thanks in
//  advance.

#define VERBOSE


// In this class, CHECKS does the following:
//  - Prints the number of translatables found for manual checking;
//  - Checks if the Translatable.Empty key is defined or not (it should not be);
//  - Checks if the Translatable.Debug key exists and is '{0}' (as it should).
//  - Checks if the selected locale has any missing keys

#define CHECKS

// TODO: Perhaps move some of this class' content to another (maybe with the
//  name 'TranslatableUtil'), since this is getting quite cluttered and now
//  looks like some standard library stuff...

using roguelike.roguelike.config;
using roguelike.roguelike.engine.handle;

namespace roguelike.roguelike.util.resources.translatable;

public class Translatable(string key)
{
  private static string activeLocale = string.Empty;
  private static Dictionary<string, string> cachedLocale = [];

  public static readonly Translatable Empty = new("translatable.empty");
  public static readonly Translatable Debug = new("translatable.debug");

  public readonly string Key = key;

  public static void Init()
  {
    // Set active locale to given config value
    activeLocale = HandlerConfig.GetConfig<ConfigMain>().ActiveLocale;

    // Cache
    cachedLocale = GenerateCache(activeLocale);

#if CHECKS
    // Check if cache was correctly generated based on the main locale en_US
    if (activeLocale != "en_US.lang")
    {
      foreach (var key in GenerateCache("en_US.lang").Keys
                 .Where(key => !cachedLocale.ContainsKey(key)))
      {
        Printf("translatable.error.missing", key);
      }
    }

    Printf("translatable.init.checks", stream: TranslatablePrintStream.Info, form: cachedLocale.Count);

    // Check if Translatable.Empty is correctly undefined and if
    //  Translatable.Debug is correctly defined as '{0}'.
    try
    {
      if (Empty.ToString() != Empty.Key)
        Print("translatable.error.definedEmpty", stream: TranslatablePrintStream.Err);
    }
    catch (KeyNotFoundException)
    {
      // Good.
    }

    if (Debug.ToString() != "{0}")
      Print("translatable.error.abnormalDebug");
#endif
  }

  private static Dictionary<string, string> GenerateCache(string name)
  {
    Dictionary<string, string> output = [];

    // Add all non-null-or-whitespace lines to the cache
    foreach (string line in File.ReadLines(Resources.GetResourcePath("lang", name)))
    {
      string trimmedLine = line.Trim();
      if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith(';')) continue;

      // Add line to dictionary
      string[] kvp = line.Split(['='], 2);
      output.Add(kvp[0].Trim().ToLower(), kvp[1].Trim());
    }

    return output;
  }

  private static string GetLangValue(Translatable translatable)
  {
    try
    {
      return cachedLocale[translatable.Key];
    }
    catch (Exception)
    {
#if SAFE_MODE
      return translatable.Key;
#else
      throw new KeyNotFoundException($"Could not find a translation key for '{translatable.Key}'.");
#endif
    }
  }

  public static void Print(Translatable translatable, bool endWithNewLine = true, TranslatablePrintStream stream =
    TranslatablePrintStream.Out, bool useKeyInsteadOfToString = false)
  {
#if !VERBOSE
      if (stream == TranslatablePrintStream.Info) return;
#endif

    Console.Write((stream == TranslatablePrintStream.Out ? string.Empty : $"[{stream.ToString().ToUpper()}] ") +
                  (useKeyInsteadOfToString ? translatable.Key : translatable.ToString()) +
                  (endWithNewLine ? Environment.NewLine : string.Empty));
  }

  public static void Print(string key, bool endWithNewLine = true, TranslatablePrintStream stream =
    TranslatablePrintStream.Out, bool parseKeyAsMessage = false)
  {
    Print(new Translatable(key), endWithNewLine, stream, parseKeyAsMessage);
  }

  public static void Printf(Translatable translatable, bool endWithNewLine = true, TranslatablePrintStream stream =
    TranslatablePrintStream.Out, params object[] form)
  {
    Print(translatable.Format(form), endWithNewLine, stream, true);
  }

  public static void Printf(string key, bool endWithNewLine = true, TranslatablePrintStream stream =
    TranslatablePrintStream.Out, params object[] form)
  {
    Printf(new Translatable(key), endWithNewLine, stream, form);
  }

  public static void Printf(Translatable translatable, params object[] formattingElements)
  {
    Printf(translatable, form: formattingElements);
  }

  public static void Printf(string key, params object[] form)
  {
    Printf(new Translatable(key), form);
  }

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
  public string Format(params object[] formatElements)
  {
    // ReSharper disable once JoinDeclarationAndInitializer
    string formattedString;
#if SAFE_MODE
      try
      {
#endif
    formattedString = string.Format(ToString(), formatElements);
#if SAFE_MODE
      }
      catch (Exception)
      {
        return ToString();
      }
#endif

    return formattedString;
  }

  public static string GetActiveLocale()
  {
    return activeLocale;
  }

  public override string ToString()
  {
    return GetLangValue(this);
  }
}
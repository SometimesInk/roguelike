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

#define CHECKS

using roguelike.roguelike.config;
using roguelike.roguelike.engine.handle;

namespace roguelike.roguelike.util.resources.translatable;

public class Translatable(string key)
{
  private static string activeLocale = string.Empty;
  private static readonly List<string> CachedLocale = [];

  public static readonly Translatable Empty = new("translatable.empty");
  public static readonly Translatable Debug = new("translatable.debug");

  public readonly string Key = key;

  public static void Init()
  {
    // Set active locale to given config value
    activeLocale = HandlerConfig.GetConfig<ConfigMain>().ActiveLocale;

    // Cache
    GenerateCache();

#if CHECKS
    // Check if cache was correctly generated
    Printf(new Translatable("translatable.init.checks"), stream: TranslatablePrintStream.Info,
      formatElements: CachedLocale.Count);

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

  private static void GenerateCache()
  {
    // Add all non-null-or-whitespace lines to the cache
    foreach (string line in File.ReadLines(Resources.GetResourcePath(Path.Combine("lang", activeLocale))))
    {
      string trimmedLine = line.Trim();
      if (!string.IsNullOrWhiteSpace(trimmedLine) && !trimmedLine.StartsWith(';')) CachedLocale.Add(trimmedLine);
    }
  }

  private static string GetLangValue(Translatable translatable)
  {
    foreach (string line in CachedLocale)
    {
      // Split into two to avoid index errors
      string[] kvp = line.Split(['='], 2);

      // Check if kvp has correctly 2 elements, and if the keys match, return
      //  a non-null value (otherwise null).
      if (kvp.Length == 2 && kvp[0].Trim().Equals(translatable.Key, StringComparison.OrdinalIgnoreCase))
        return kvp[1].Trim();
    }

#if SAFE_MODE
      return translatable.Key;
#else
    throw new KeyNotFoundException($"Could not find a translation key for '{translatable.Key}'.");
#endif
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
    TranslatablePrintStream.Out, params object[] formatElements)
  {
    Print(translatable.Format(formatElements), endWithNewLine, stream, true);
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
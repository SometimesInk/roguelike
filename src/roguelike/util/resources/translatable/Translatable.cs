// In this class, SAFE MODE does the following:
//  - It makes the GetLangValue(...) not throw an exception on failure, but
//    instead return the translatable's key.
//  - It makes the Format(...) method not throw an exception on failure, but
//    instead returns the unformatted value.

// #define SAFE_MODE

// When VERBOSE is not defined, the Log() method will not do anything. Thanks in
//  advance.

#define VERBOSE

namespace roguelike.roguelike.util.resources.translatable
  // ReSharper disable once ArrangeNamespaceBody
{
  public class Translatable(string key)
  {
    public static string ActiveLocale = null!;

    public static readonly Translatable Empty = new("translatable.empty");
    public static readonly Translatable Debug = new("translatable.debug");

    public readonly string Key = key;

    // ReSharper disable once CommentTypo
    /// <summary>
    /// This method reads the active language locale file to find the value of
    /// the translatable.
    /// </summary>
    /// <param name="translatable">The translatable whose value shall be
    /// returned.</param>
    /// <returns>The translatable value or, if not key was found and safe mode
    /// is defined, the given translatable's key.</returns>
    /// <exception cref="KeyNotFoundException">If safe mode is undefined and no
    /// translation key is found.</exception>
    private static string GetLangValue(Translatable translatable)
    {
      using StreamReader reader = new(Resources.GetResourcePath("lang/" + ActiveLocale));

      // Process the file line by line
      while (!reader.EndOfStream)
      {
        string? line = reader.ReadLine()?.Trim();

        // Skip empty and comment lines
        if (string.IsNullOrWhiteSpace(line) || ";".StartsWith(line))
          continue;

        // Get kvp
        string[] kvp = line.Split(['='], 2); // Split only into two parts to avoid index errors
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
      Print(Debug.Format(message), true, TranslatablePrintStream.Info, true);
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

    public override string ToString()
    {
      return GetLangValue(this);
    }
  }
}
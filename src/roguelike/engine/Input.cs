using roguelike.roguelike.engine.command;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine;

internal static class Input
{
  /// <summary>
  /// Asks the user to use a command.
  /// </summary>
  /// <param name="parser">Command parser to use.</param>
  /// <param name="win">Window to modify if need be.</param>
  /// <returns>Whether the window should be redrawn.</returns>
  public static void Ask(CommandParser parser, Window win)
  {
    Translatable.Printf(new Translatable("command.parsing.invite"), endWithNewLine: false, formatElements: " ");
    string? line = Console.ReadLine();

    // Check if line is valid
    if (string.IsNullOrWhiteSpace(line))
    {
      win.SetWindowShouldRedraw(false);
      return;
    }

    // Get command arguments where arg[0] is the command alias used
    string[] args = line.Split(' ');

    // Send invalid translatable if the command wasn't recognized and parsed.
    CommandResult? output = parser.ParseCommand(win, args);
    if (output == null)
      Translatable.Printf(new Translatable("command.parsing.invalid"), formatElements: args[0]);
    else
      Translatable.Print(output.Message, useKeyInsteadOfToString: output.UseKeyForMessage);
  }
}
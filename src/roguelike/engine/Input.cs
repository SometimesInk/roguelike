using roguelike.roguelike.engine.handle;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine;

internal static class Input
{
  public static void Ask()
  {
    Translatable.Printf(new Translatable("command.parsing.invite"), endWithNewLine: false, formatElements: " ");
    string? line = Console.ReadLine();

    // Check if line is valid
    if (string.IsNullOrWhiteSpace(line))
    {
      Window.ShouldRedraw = false;
      return;
    }

    // Get command arguments where arg[0] is the command alias used
    string[] args = line.Split(' ');

    // Send invalid translatable if the command wasn't recognized and parsed.
    var output = HandlerCommand.ParseCommand(args);
    if (output.Item1 == Translatable.Empty)
      Translatable.Printf(new Translatable("command.parsing.invalid"), formatElements: args[0]);
    else
    {
      if (output.Item2 == null) Translatable.Print(output.Item1);
      else Translatable.Printf(output.Item1, formatElements: output.Item2);
    }
  }
}
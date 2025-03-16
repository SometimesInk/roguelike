using roguelike.roguelike.engine.commands;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine;

internal static class Input
{
  public static void Ask(EntryWindow win)
  {
    win.WriteMessage(new Translatable("command.parsing.invite").Format(' ', ' '));

    bool acquiringInput = true;
    while (acquiringInput)
    {
      // Intercept key presses (such that there is no echo)
      // TODO: Make this safe (since it intercepts CTRL-C)
      ConsoleKeyInfo key = Console.ReadKey(true);

      win.ModifyInput(key, s =>
      {
        ParseBuffer(win, s);
        acquiringInput = false;
      });
    }
  }

  private static void ParseBuffer(EntryWindow win, string input)
  {
    // Check if buffer is whitespace
    if (string.IsNullOrWhiteSpace(input))
    {
      Window.ShouldRedraw = false;
      return;
    }

    // Get command arguments where arg[0] is the command alias used
    string[] args = input.Split(' ');

    // Send invalid translatable if the command wasn't recognized and parsed.
    Translatable output = CommandHandler.ParseCommand(win, args);

    // Format command output
    win.WriteMessage(output == Translatable.Empty
      ? new Translatable("command.parsing.invalid").Format(args[0])
      : output);
  }
}
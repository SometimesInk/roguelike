using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine;

internal static class Input
{
  public static void Ask(EntryWindow win)
  {
    win.WriteMessage(new Translatable("command.parsing.invite").Format(' '),
      newLineAfter: false, moveCursor: true);

    // Intercept key presses (such that there is no echo)
    bool stop = false;
    while (!stop && !Window.ShouldClose) stop = win.ModifyInput(win, Console.ReadKey(true));
  }
}
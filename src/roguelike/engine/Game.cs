// In this class, CHECKS does the following:
//  - Checks if the Translatable.Empty key is defined or not (it should not be);
//  - Checks if the Translatable.Debug key exists and is '{0}' (as it should).

#define CHECKS

using roguelike.roguelike.engine.command;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine
  // ReSharper disable once ArrangeNamespaceBody
{
  internal class Game
  {
    private readonly CommandParser parser;
    private readonly Window win;

    public Game()
    {
      // Configure locale
      Translatable.ActiveLocale = "en_US.lang";

#if CHECKS
      // Check if Translatable.Empty is correctly undefined and if
      //  Translatable.Debug is correctly defined as '{0}'.
      try
      {
        if (Translatable.Empty.ToString() != Translatable.Empty.Key)
          Translatable.Print("translatable.error.definedEmpty", stream: TranslatablePrintStream.Err);
      }
      catch (KeyNotFoundException)
      {
        // Good.
      }

      if (Translatable.Debug.ToString() != "{0}")
        Translatable.Print("translatable.error.abnormalDebug");
#endif

      // Init
      parser = new CommandParser();
      win = new Window();
    }

    public void Run()
    {
      win.SetWindowShouldRedraw(true);
      while (!win.WindowShouldClose())
      {
        Loop();
      }
    }

    private void Loop()
    {
      // Draw window if it is required
      if (win.WindowShouldRedraw())
        Draw();
      else
        win.SetWindowShouldRedraw(true);

      // Ask for command input
      Input.Ask(parser, win);

      // If the window should be redrawn, ask for a keypress before doing so.
      if (!win.WindowShouldRedraw()) return;

      // Ask for a keypress before continuation
      Translatable.Print("utils.key");
      Console.ReadKey();
    }

    private void Draw()
    {
      Console.Clear();
    }
  }
}
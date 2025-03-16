using roguelike.roguelike.engine.commands;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.configuring;
using roguelike.roguelike.utils.resources.locale;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine
{
  internal static class Game
  {
    private static EntryWindow logs = new EntryWindow("Logs", 0, (ushort)(Console.WindowHeight - 4),
      (ushort)(Console.WindowWidth - 1), 3);

    private static EntryWindow mainWindow = new EntryWindow("Main Terminal", 0, 0,
      (ushort)(Console.WindowWidth - 1), (ushort)(Console.WindowHeight - 4));

    public static void Run()
    {
      // Initialize essential components before others (pre-initialization)
      ConfigHandler.Init(mainWindow);
      LocaleHandler.Init(mainWindow);

      // Initialize the rest
      CommandHandler.Init(mainWindow);
      Window.Init();

      // Run game loop
      Draw();
      while (!Window.ShouldClose) Loop();
    }

    private static void Loop()
    {
      // Draw window if it is required
      if (Window.ShouldRedraw)
        Draw();
      else
        Window.ShouldRedraw = true;

      // Ask for command input
      Input.Ask(mainWindow);

      // If the window should be redrawn, ask for a keypress before doing so.
      if (!Window.ShouldRedraw) return;

      // Ask for a keypress before continuation
      mainWindow.WriteMessage(new Translatable("utils.key").Format("    "));
      Console.ReadKey();
    }

    private static void Draw()
    {
      // TODO: Change window clear to partial clear, perhaps using
      //  a new RenderBox#Clear() which would fill its contents with spaces
      Console.Clear();
      mainWindow.Clear();

      // Rendering
      mainWindow.RenderBoundingBox();
      logs.RenderBoundingBox();
    }
  }
}
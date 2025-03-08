using roguelike.roguelike.engine.handle;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine
{
  internal static class Game
  {
    public static void Run()
    {
      PreInit();
      Init();

      // Run game loop
      while (!Window.ShouldClose) Loop();
    }

    /// <summary>
    /// Initialization for absolutely essential components
    /// </summary>
    private static void PreInit()
    {
      HandlerConfig.Init();
      Translatable.Init();
    }

    private static void Init()
    {
      HandlerCommand.Init();
      Window.Init();
    }


    private static void Loop()
    {
      // Draw window if it is required
      if (Window.ShouldRedraw)
        Draw();
      else
        Window.ShouldRedraw = true;

      // Ask for command input
      Input.Ask();

      // If the window should be redrawn, ask for a keypress before doing so.
      if (!Window.ShouldRedraw) return;

      // Ask for a keypress before continuation
      Translatable.Print("utils.key");
      Console.ReadKey();
    }

    private static void Draw()
    {
      Console.Clear();
    }
  }
}
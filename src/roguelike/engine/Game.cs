using roguelike.roguelike.engine.commands;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.configuring;
using roguelike.roguelike.utils.resources.locale;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine;

internal static class Game
{
  public static void Run()
  {
    // Initialization
    ConfigHandler.Init(Window.Logs);
    LocaleHandler.Init(Window.Logs);
    Window.Init();
    CommandHandler.Init(Window.Logs);

    // Run game loop
    Draw();
    while (!Window.ShouldClose) Loop();
    Quit();
  }

  private static void Loop()
  {
    // Draw window if it is required
    if (Window.ShouldRedraw)
      Draw();
    else
      Window.ShouldRedraw = true;

    // Ask for command input
    Input.Ask(Window.Focused);
    if (Window.ShouldClose) return;

    // If the window should be redrawn, ask for a keypress before doing so.
    if (!Window.ShouldRedraw) return;

    // Ask for a keypress before continuation
    Window.Focused.WriteMessage(new Translatable("utils.key").Format("    "));
    _ = Console.ReadKey(true);
  }

  private static void Draw()
  {
    Window.ActiveWindows.ForEach(window => { window.Clear(false); });
  }

  private static void Quit()
  {
    Console.Clear();

    // Save what there is to save
  }
}
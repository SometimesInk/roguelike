namespace roguelike.roguelike.engine;

public static class Window
{
  public static bool ShouldClose { get; set; }

  public static bool ShouldRedraw { get; set; }

  public static void Init()
  {
    ShouldClose = false;
    ShouldRedraw = false;
  }
}
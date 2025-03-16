using roguelike.roguelike.utils.maths;

namespace roguelike.roguelike.engine.rendering;

public static class Window
{
  public static bool ShouldClose { get; set; }

  public static bool ShouldRedraw { get; set; }

  public static void Init()
  {
    ShouldClose = false;
    ShouldRedraw = false;
  }

  public static bool Contains(Point point, out Point? overflow)
  {
    overflow = null;

    // Check if point is contained
    if (point.X <= Console.WindowWidth && point.Y <= Console.WindowHeight) return true;

    // Calculate overflow
    overflow = new Point((ushort)(point.X - (Console.WindowWidth - 1)), (ushort)(point.Y - (Console.WindowHeight - 1)));
    return false;
  }
}
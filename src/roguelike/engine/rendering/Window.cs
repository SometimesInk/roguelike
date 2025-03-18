using roguelike.roguelike.utils.maths;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.rendering;

public static class Window
{
  public static EntryWindow Focused;

  public static EntryWindow MainWindow = new(new Translatable("window.main.name"), 0, 0,
    (ushort)(Console.WindowWidth - 1), (ushort)(Console.WindowHeight - 3));

  public static EntryWindow Logs = new(new Translatable("window.logs.name"), 0,
    (ushort)(Console.WindowHeight - 3), (ushort)(Console.WindowWidth - 1), 2);

  public static List<EntryWindow> ActiveWindows = [MainWindow, Logs];

  public static bool ShouldClose { get; set; }

  public static bool ShouldRedraw { get; set; }

  /// <summary>
  /// Initializes window components.
  /// </summary>
  /// <remarks>Requires Locale.</remarks>
  public static void Init()
  {
    Focused = MainWindow;

    ShouldClose = false;
    ShouldRedraw = false;

    Console.CancelKeyPress += (_, args) =>
    {
      args.Cancel = true;
      ShouldClose = true;
      ShouldRedraw = false;
    };

    ActiveWindows.ForEach(win => win.RenderBoundingBox(moveCursor: false));
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

  private static Direction ParseKeyToDirection(ConsoleKeyInfo arrow)
  {
    return arrow.Key switch
    {
      ConsoleKey.LeftArrow => Direction.Left,
      ConsoleKey.UpArrow => Direction.Up,
      ConsoleKey.RightArrow => Direction.Right,
      ConsoleKey.DownArrow => Direction.Down,
      _ => throw new ArgumentException("Unexpected key"),
    };
  }

  // TODO: Handle resizing

  public static void MoveFocus(ConsoleKeyInfo arrow)
  {
    Direction dir = ParseKeyToDirection(arrow);

    // Move focus
  }
}
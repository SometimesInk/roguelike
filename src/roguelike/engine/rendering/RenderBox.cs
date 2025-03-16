using roguelike.roguelike.utils.maths;

namespace roguelike.roguelike.engine.rendering;

public class RenderBox
{
  private static List<(char, Point)> corners = [];

  public Point Start;
  public ushort Width;
  public ushort Height;

  private List<(char, Point)> theseCorners = [];

  public RenderBox(Point start, ushort width, ushort height)
  {
    Start = start;
    Width = width;
    Height = height;

    // Check if it fits
    if (!Window.Contains(start, out _)) throw new IndexOutOfRangeException("Point is out of window.");
    if (Window.Contains(new Point((ushort)(start.X + width), (ushort)(start.Y + height)), out Point? overflow)) return;

    // Move point within bounds - overflow cannot be null when method returns false
    Width = (ushort)(width - overflow.X);
    Height = (ushort)(height - overflow.Y);
  }

  public RenderBox(ushort x, ushort y, ushort width, ushort height) : this(new Point(x, y), width, height)
  {
  }

  public bool Contains(Point p)
  {
    return Start.X <= p.X && p.X <= Start.X + Width && Start.Y <= p.Y && p.Y <= Start.Y + Height;
  }

  /// <summary>
  /// Renders a bounding box and puts the cursor at the current working location
  /// </summary>
  public virtual void RenderBoundingBox(string? name = null, bool moveCursor = true)
  {
    // Corners
    theseCorners.ForEach(c => corners.Remove(c));
    theseCorners.Clear();
    theseCorners.Add(Corner(Start.X, Start.Y, '┌'));
    theseCorners.Add(Corner((ushort)(Start.X + Width), Start.Y, '┐'));
    theseCorners.Add(Corner(Start.X, (ushort)(Start.Y + Height), '└'));
    theseCorners.Add(Corner((ushort)(Start.X + Width), (ushort)(Start.Y + Height), '┘'));
    foreach ((char, Point) corner in theseCorners)
    {
      Console.SetCursorPosition(corner.Item2.X, corner.Item2.Y);
      Console.Write(corner.Item1);
    }

    corners.AddRange(theseCorners);

    // First stroke down
    for (int i = 1; i < Height; i++)
    {
      Console.SetCursorPosition(Start.X, Start.Y + i);
      Console.Write("│");
    }

    // Second stroke down
    for (int i = 1; i < Height; i++)
    {
      Console.SetCursorPosition(Start.X + Width, Start.Y + i);
      Console.Write("│");
    }

    // Top middle stroke
    for (int i = 1; i < Width; i++)
    {
      Console.SetCursorPosition(Start.X + i, Start.Y);
      Console.Write("─");
    }

    // Down middle stroke
    for (int i = 1; i < Width; i++)
    {
      Console.SetCursorPosition(Start.X + i, Start.Y + Height);
      Console.Write("─");
    }

    // Set window name
    if (name != null)
    {
      Console.SetCursorPosition(Start.X + 1, Start.Y);
      // Console.Write($"{Width}─{Height}"); // Debugging
      Console.Write(name.Replace(" ", "─"));
    }

    // Move cursor inside the box
    if (moveCursor) Console.SetCursorPosition(Start.X + 1, Start.Y + 1);
  }

  private static (char, Point) Corner(ushort x, ushort y, char cornerBase)
  {
    Point c = new(x, y);
    char cornerOn = 'o';
    foreach ((char, Point) corner in corners.Where(corner => corner.Item2 == c)) cornerOn = corner.Item1;

    return cornerOn switch
    {
      '┌' => cornerBase switch
      {
        '┌' => ('┌', c),
        '┐' => ('┬', c),
        '└' => ('├', c),
        '┘' => ('┼', c),
        _ => (cornerBase, c)
      },
      '┐' => cornerBase switch
      {
        '┌' => ('┬', c),
        '┐' => ('┐', c),
        '└' => ('┼', c),
        '┘' => ('┤', c),
        _ => (cornerBase, c)
      },
      '└' => cornerBase switch
      {
        '┌' => ('├', c),
        '┐' => ('┼', c),
        '└' => ('└', c),
        '┘' => ('┴', c),
        _ => (cornerBase, c)
      },
      '┘' => cornerBase switch
      {
        '┌' => ('┼', c),
        '┐' => ('┤', c),
        '└' => ('┴', c),
        '┘' => ('┘', c),
        _ => (cornerBase, c)
      },
      _ => (cornerBase, c)
    };
  }
}
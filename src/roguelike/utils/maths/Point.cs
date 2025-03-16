using System.Numerics;

namespace roguelike.roguelike.utils.maths;

/// <summary>
/// Represents a point in the 2-dimensional Terminal Space
/// </summary>
public record Point
  : IAdditionOperators<Point, Point, Point>, IAdditionOperators<Point, ushort, Point>
{
  public static readonly Point Origin = new(0, 0);

  public ushort X;
  public ushort Y;

  public Point(ushort x, ushort y)
  {
    X = x;
    Y = y;
  }

  public static Point operator +(Point left, Point right)
  {
    return new Point((ushort)(left.X + right.X), (ushort)(left.Y + right.Y));
  }

  public static Point operator +(Point left, ushort right)
  {
    return new Point((ushort)(left.X + right), (ushort)(left.Y + right));
  }
}
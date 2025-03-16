namespace roguelike.roguelike.utils.maths;

public class RelativePoint
{
  public Point Point;
  public Point Origin;

  public RelativePoint(Point point, Point origin)
  {
    Point = point;
    Origin = origin;
  }

  public RelativePoint(ushort px, ushort py, ushort ox, ushort oy)
  {
    Point = new Point(px, py);
    Origin = new Point(ox, oy);
  }

  public Point GetUniversalCoordinates()
  {
    return new Point((ushort)(Point.X + Origin.X), (ushort)(Point.Y + Origin.Y));
  }
}
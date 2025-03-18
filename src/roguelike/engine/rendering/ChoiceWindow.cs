using roguelike.roguelike.utils.maths;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.rendering;

public class ChoiceWindow : RenderBox
{
  private readonly Translatable windowName;

  public ChoiceWindow(Translatable windowName, Point start, ushort width, ushort height) : base(start, width, height)
  {
    this.windowName = windowName;
  }

  public ChoiceWindow(Translatable windowName, ushort x, ushort y, ushort width, ushort height)
    : base(x, y, width, height)
  {
    this.windowName = windowName;
  }

  public override void RenderBoundingBox(string? name = null, bool moveCursor = true)
  {
    base.RenderBoundingBox(name ?? windowName.ToString(), moveCursor);
  }

  // TODO: choices :P
}
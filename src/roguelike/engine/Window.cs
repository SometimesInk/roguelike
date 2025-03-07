namespace roguelike.roguelike.engine;

public class Window
{
  private bool shouldClose;
  private bool shouldRedraw;

  public void SetWindowShouldClose(bool value)
  {
    shouldClose = value;
  }

  public void SetWindowShouldRedraw(bool value)
  {
    shouldRedraw = value;
  }

  public bool WindowShouldClose()
  {
    return shouldClose;
  }

  public bool WindowShouldRedraw()
  {
    return shouldRedraw;
  }
}
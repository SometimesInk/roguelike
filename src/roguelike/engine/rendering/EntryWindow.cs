using roguelike.roguelike.utils.maths;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.rendering;

public class EntryWindow : RenderBox
{
  private readonly string name;

  private string input = string.Empty;
  private RelativePoint cursor;

  #region Constructors

  // TODO: Translatable
  public EntryWindow(string name, Point start, ushort width, ushort height) : base(start, width, height)
  {
    this.name = name;
    cursor = new RelativePoint(Point.Origin, start + 1);
  }

  // TODO: Translatable
  public EntryWindow(string name, ushort x, ushort y, ushort width, ushort height) : base(x, y, width, height)
  {
    this.name = name;
    cursor = new RelativePoint(0, 0, (ushort)(x + 1), (ushort)(y + 1));
  }

  #endregion

  public override void RenderBoundingBox(string? name = null, bool moveCursor = true)
  {
    base.RenderBoundingBox(name ?? this.name, moveCursor);
  }

  public void ModifyInput(ConsoleKeyInfo key, Action<string> parseEnter)
  {
    if (key.Key == ConsoleKey.Backspace)
    {
      // Check if there is at least a character to remove
      if (input.Length <= 0) return;

      // Remove last character
      input = input[..^1];

      // Erase the character visually. This works even though the backspace
      //  character is non-destructive on most terminals, since there is space
      //  replacing the previous character.
      Console.Write("\b \b");
      cursor.Point.X = (ushort)(cursor.Point.X - 1);
      return;
    }

    if (key.Key == ConsoleKey.Enter)
    {
      // Check if buffer is empty
      if (string.IsNullOrEmpty(input)) return;

      // Set cursor on new line
      // cursor.Point.Y = (ushort)(cursor.Point.Y + 1);
      cursor.Point.X = 0;

      // Parse buffer
      parseEnter(input);
      input = string.Empty;
      return;
    }

    // Add char to buffer
    input += key.KeyChar;
    Console.Write(key.KeyChar);
    cursor.Point.X++;
  }

  // TODO: Translatables
  // TODO: Line wrapping
  public void WriteMessage(Translatable translatable, TranslatablePrintStream stream = TranslatablePrintStream.Out,
    bool newLineAfter = true)
  {
    string message = translatable.ToString();
    // Write
    Point universalCoordinates = cursor.GetUniversalCoordinates();
    Console.SetCursorPosition(universalCoordinates.X, universalCoordinates.Y);
    Console.Write(message);

    // Move cursor for next instance
    if (newLineAfter)
    {
      cursor.Point.X = 0;

      // Check if it is possible to move the cursor down
      if ((ushort)(cursor.Point.Y + 1) >= Height - 2) return;

      cursor.Point.Y = (ushort)(cursor.Point.Y + 1);
      return;
    }

    cursor.Point.X = (ushort)(cursor.Point.X + message.Length);
  }

  public void Clear()
  {
    cursor.Point.X = 0;
    cursor.Point.Y = 0;
  }
}
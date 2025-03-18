using roguelike.roguelike.engine.commands;
using roguelike.roguelike.utils.maths;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.rendering;

public class EntryWindow : RenderBox
{
  private readonly Translatable windowName;

  private string input = string.Empty;
  private RelativePoint cursor;

  private List<string> lines = [];

  public EntryWindow(Translatable windowName, Point start, ushort width, ushort height) : base(start, width, height)
  {
    this.windowName = windowName;
    cursor = new RelativePoint(Point.Origin, start + 1);
  }

  public EntryWindow(Translatable windowName, ushort x, ushort y, ushort width, ushort height)
    : base(x, y, width, height)
  {
    this.windowName = windowName;
    cursor = new RelativePoint(0, 0, (ushort)(x + 2), (ushort)(y + 1));
  }

  #region Input Parsing

  private bool HandleRegularInput(EntryWindow win, ConsoleKeyInfo key)
  {
    // Add char to buffer
    input += key.KeyChar;
    Console.Write(key.KeyChar);
    cursor.Point.X++;
    return false;
  }

  private bool HandleBackspace(EntryWindow win, ConsoleKeyInfo key)
  {
    // Check if there is at least a character to remove
    if (input.Length <= 0) return false;

    // Remove last character
    input = input[..^1];

    // Erase the character visually. This works even though the backspace
    //  character is non-destructive on most terminals, since there is space
    //  replacing the previous character.
    Console.Write("\b \b");
    cursor.Point.X = (ushort)(cursor.Point.X - 1);
    return false;
  }

  private bool HandleEnter(EntryWindow win, ConsoleKeyInfo key)
  {
    // Check if buffer is empty
    if (string.IsNullOrEmpty(input)) return false;

    // Set cursor on new line
    // cursor.Point.Y = (ushort)(cursor.Point.Y + 1);
    cursor.Point.X = 0;
    cursor.Point.Y = (ushort)(cursor.Point.Y + 1);

    // Parse buffer
    CommandHandler.ParseBuffer(win, input);

    // Add input to lines
    string line = lines.Find(c => c == new Translatable("command.parsing.invite").Format(' ').ToString())
                  ?? string.Empty;
    lines.Remove(line);
    lines.Insert(0, line + input);

    input = string.Empty;
    return true;
  }

  private bool HandleArrowKeys(EntryWindow win, ConsoleKeyInfo key)
  {
    Window.MoveFocus(key);
    return false;
  }

  public bool ModifyInput(EntryWindow win, ConsoleKeyInfo key)
  {
    return key.Key switch
    {
      ConsoleKey.Backspace => HandleBackspace(win, key),
      ConsoleKey.Enter => HandleEnter(win, key),
      ConsoleKey.UpArrow or ConsoleKey.DownArrow or ConsoleKey.LeftArrow or ConsoleKey.RightArrow
        => HandleArrowKeys(win, key),
      _ => HandleRegularInput(win, key),
    };
  }

  #endregion

  #region Rendering

  public override void RenderBoundingBox(string? name = null, bool moveCursor = true)
  {
    base.RenderBoundingBox(name ?? windowName.ToString(), moveCursor);
  }

  // TODO: Line overflow
  // TODO: Line wrapping
  public void WriteMessage(Translatable translatable, TranslatablePrintStream stream = TranslatablePrintStream.Out,
    bool newLineAfter = true, bool moveCursor = false)
  {
    string message = translatable.ToString();
    Point universalCoordinates = cursor.GetUniversalCoordinates();

    // Write message
    Console.SetCursorPosition(universalCoordinates.X, universalCoordinates.Y);
    Console.Write(message);
    lines.Add(message);

    // Move cursor for next instance
    if (newLineAfter)
    {
      cursor.Point.X = 0;

      // Check if it is possible to move the cursor down
      if ((ushort)(cursor.Point.Y + 1) < Height - 1) cursor.Point.Y = (ushort)(cursor.Point.Y + 1);
    }
    else cursor.Point.X = (ushort)(cursor.Point.X + message.Length);

    universalCoordinates = cursor.GetUniversalCoordinates();
    if (moveCursor) Console.SetCursorPosition(universalCoordinates.X, universalCoordinates.Y);
  }

  public void Clear(bool moveCursor)
  {
    // Erase all lines
    for (var i = 0; i < Math.Min(lines.Count, Height - 1); i++)
    {
      // Move cursor to line
      Console.SetCursorPosition(cursor.Origin.X, (ushort)(cursor.Origin.Y + i));

      // Erase all characters
      Console.Write(new string(' ', lines[i].Length));
    }

    lines.Clear();

    // Reset cursor
    cursor.Point.X = 0;
    cursor.Point.Y = 0;
    if (moveCursor) Console.SetCursorPosition(cursor.Origin.X, cursor.Origin.Y);
  }

  #endregion

}
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command;

public class CommandResult(Translatable output, bool useKeyForMessage = false, bool closeWindow = false)
{
  public Translatable Message { get; } = output;
  public bool UseKeyForMessage { get; } = useKeyForMessage;
  public bool CloseWindow { get; } = closeWindow;

  public static CommandResult Format(Translatable translatable, bool closeWindow = false,
    params object[] formattingElements)
  {
    return new CommandResult(new Translatable(translatable.Format(formattingElements)), true,
      closeWindow);
  }
}
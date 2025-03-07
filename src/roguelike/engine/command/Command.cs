using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command;

public abstract class Command
{
  protected abstract CommandType GetCommandType();

  protected abstract string GetName();

  // ReSharper disable once MemberCanBeProtected.Global
  public Translatable GetTranslatable(string argument = "name")
  {
    return new Translatable("command." + GetCommandType().ToString().ToLower() + "." + GetName() + "." + argument);
  }

  public string GetUsage()
  {
    return GetTranslatable().ToString() + GetTranslatable("usage");
  }

  /// <summary>
  /// Executes the command for the given arguments.
  /// </summary>
  /// <param name="args">Arguments to parse the command for, where the 0th index
  /// is the command alias used.</param>
  /// <returns>A CommandResult object indicating how to process the command
  /// after execution.</returns>
  public abstract CommandResult Execute(string[] args);

  public override string ToString()
  {
    CommandType type = GetCommandType();
    return (type == CommandType.Common ? "" : (char)type) + GetTranslatable().ToString();
  }
}
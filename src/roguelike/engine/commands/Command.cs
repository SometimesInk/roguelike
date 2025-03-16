using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands;

public abstract class Command
{
  protected abstract CommandType GetCommandType();

  protected abstract string GetName();

  public Translatable GetTranslatable(string argument = "name")
  {
    return new Translatable("command." + GetCommandType().ToString().ToLower() + "." + GetName() + "." + argument);
  }

  public string GetUsage()
  {
    return GetTranslatable() + " -> " + GetTranslatable("usage");
  }

  /// <summary>
  /// Executes the command for the given arguments.
  /// </summary>
  /// <param name="win">Window for potential logging,</param>
  /// <param name="args">Arguments to parse the command for, where the 0th index
  ///   is the command alias used.</param>
  /// <returns>A CommandResult object indicating how to process the command
  /// after execution.</returns>
  public abstract Translatable Execute(EntryWindow win, string[] args);

  public override string ToString()
  {
    CommandType type = GetCommandType();
    return (type == CommandType.Common ? "" : (char)type) + GetTranslatable().ToString();
  }
}
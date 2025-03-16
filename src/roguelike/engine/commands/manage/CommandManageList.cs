using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

public class CommandManageList : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "list";
  }

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    // Check arguments
    if (args.Length != 1) return GetTranslatable("error.toManyArgs");

    // Read out files in the directory
    int count = 0;
    foreach (Command command in CommandHandler.RegisteredCommands)
    {
      win.WriteMessage(Translatable.Debug.Format(command.GetUsage()));
      count++;
    }

    return GetTranslatable("output").Format(count);
  }
}
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command.manage;

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

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // Check arguments
    if (args.Length != 1) return (GetTranslatable("error.toManyArgs"), null);

    // Read out files in the directory
    int count = 0;
    foreach (Command command in HandlerCommand.RegisteredCommands)
    {
      Translatable.Printf(Translatable.Debug, command.GetUsage());
      count++;
    }

    return (GetTranslatable("output"), [count]);
  }
}
namespace roguelike.roguelike.engine.command.manage;

public class CommandManageQuit : Command
{
  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "quit";
  }

  public override CommandResult Execute(string[] args)
  {
    return new CommandResult(GetTranslatable("output"), true);
  }
}
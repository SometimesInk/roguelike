using roguelike.roguelike.util.resources.translatable;

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

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // Close window
    Window.ShouldClose = true;
    return (GetTranslatable("output"), null);
  }
}
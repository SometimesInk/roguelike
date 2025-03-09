using roguelike.roguelike.networking;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command.manage;

public class CommandManageNetInit : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "net.init";
  }

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // TODO: Check arguments
    // TODO: Check for errors in the connection
    HandlerNetwork.StartServer();

    return (GetTranslatable("output"), null);
  }
}
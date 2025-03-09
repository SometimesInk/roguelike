using roguelike.roguelike.networking;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command.manage;

public class CommandManageNetJoin : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "net.join";
  }

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // TODO: Check arguments
    // TODO: Check for errors in the connection
    HandlerNetwork.ConnectToServer(args[1], Convert.ToInt32(args[2]));

    return (GetTranslatable("output"), null);
  }
}
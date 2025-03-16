using roguelike.roguelike.engine.networking;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

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

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    // TODO: Check arguments
    // TODO: Check for errors in the connection
    HandlerNetwork.ConnectToServer(args[1], Convert.ToInt32(args[2]));

    return GetTranslatable("output");
  }
}
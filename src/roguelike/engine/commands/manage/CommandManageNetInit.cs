using roguelike.roguelike.engine.networking;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

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

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    // TODO: Check arguments
    // TODO: Check for errors in the connection
    HandlerNetwork.StartServer(win);

    return GetTranslatable("output");
  }
}
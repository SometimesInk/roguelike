using System.Net.Sockets;
using roguelike.roguelike.engine.networking;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

public class CommandManageNetListClients : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "net.listClients";
  }

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    // Check arguments
    if (args.Length != 1) return GetTranslatable("error.toManyArgs");

    // Read out files in the directory
    int count = 0;
    try
    {
      // Print the address of every client
      foreach (TcpClient client in HandlerNetwork.GetClients())
      {
        // TODO: Get client Uid or IP-address for stronger debugging and make
        //  an actual translatable like @langList
        win.WriteMessage(Translatable.Debug.Format("client"));
        count++;
      }
    }
    catch (Exception)
    {
      return GetTranslatable("error.notServer");
    }

    return GetTranslatable("output").Format(count);
  }
}
using System.Net.Sockets;
using roguelike.roguelike.networking;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command.manage;

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

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // Check arguments
    if (args.Length != 1) return (GetTranslatable("error.toManyArgs"), null);

    // Read out files in the directory
    int count = 0;
    try
    {
      // Print the address of every client
      foreach (TcpClient client in HandlerNetwork.GetClients())
      {
        Translatable.Printf(Translatable.Debug, "client");
        count++;
      }
    }
    catch (Exception)
    {
      return (GetTranslatable("error.notServer"), null);
    }

    return (GetTranslatable("output"), [count]);
  }
}
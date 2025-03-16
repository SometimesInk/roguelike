using roguelike.roguelike.engine.networking;
using roguelike.roguelike.engine.networking.packets;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

public class CommandManageNetSendPacketH : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "net.sendPacketH";
  }

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    try
    {
      Packet packet = new((PacketType)Enum.Parse(typeof(PacketType), args[1]));

      HandlerNetwork.SendPacketToHost(packet);
    }
    catch (Exception)
    {
      return GetTranslatable("error");
    }

    return GetTranslatable("output");
  }
}
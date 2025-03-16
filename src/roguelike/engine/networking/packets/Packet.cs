using roguelike.roguelike.utils.resources.translatables;
using roguelike.roguelike.utils.types;

namespace roguelike.roguelike.engine.networking.packets;

public class Packet
{
  public PacketType Type;
  public Uid Sender;
  public ushort Id;

  public byte[] Serialize()
  {
    Translatable.Log($"{Type.ToString()}");
    return [(byte)Type, Sender.Id, ..BitConverter.GetBytes(Id)];
  }

  public static Packet Deserialize(byte[] data)
  {
    Translatable.Log(BitConverter.ToString(data));

    PacketType type = (PacketType)Enum.ToObject(typeof(PacketType), data[0]);
    Uid sender = new Uid(data[1]);

    //ushort id = BitConverter.ToUInt16(data.AsSpan()[2..3]);
    ushort id = 36;

    Packet packet = new Packet(type, sender, id);

    return packet;
  }

  public Packet(PacketType type, Uid sender, ushort id)
  {
    Type = type;
    Sender = sender;
    Id = id;
  }

  public Packet(PacketType type)
  {
    Type = type;

    // Request Uid
    Sender = new Uid(36);

    // Get id
    Id = 26;
  }
}
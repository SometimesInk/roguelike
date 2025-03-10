namespace roguelike.roguelike.networking.packet;

public enum PacketType : byte
{
  // Chat Packets 0b0010...
  ChatGlobal = 0x20,
  ChatPrivate,

  // Network Packets 0b0100...
  /// <summary>
  /// Packet sent by the receiver to confirm that the previous message was received
  /// </summary>
  Ack = 0x40,

  /// <summary>
  /// Packet sent by the server to refuse a request
  /// </summary>
  Refusal,
}
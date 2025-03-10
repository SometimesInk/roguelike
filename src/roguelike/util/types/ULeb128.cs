namespace roguelike.roguelike.util.types;

public class ULeb128
{
  /// <summary>
  /// Byte whose most significant bit is 1
  /// <code>
  /// 0b1000_0000 - 0d128 - 0x80
  /// </code>
  /// </summary>
  private const byte Msb = 0x80;

  public static void WriteULeb128(Stream stream, ulong value, out int numberOfBytes)
  {
    numberOfBytes = 0;
    bool lastByte = false;

    while (!lastByte)
    {
      numberOfBytes++;

      // Get the least significant byte (LSB) with the last bit (first from the
      //  right) set to the value 1
      // This works because XXX... OR 1000... is 1XXX...
      byte next = (byte)(value | Msb);
      value >>= 7; // Shift value to fill empty space

      // Check if this is the last byte
      lastByte = value == 0;

      // Force the most significant bit to 0. This works because the MSB is
      //  forced above to the value 1, thus 1XXX... XOR 1000... is 0XXX...
      if (lastByte) next ^= Msb;

      // Write byte to stream
      stream.WriteByte(next);
    }
  }

  public static ulong ReadULeb128(Stream stream, out int numberOfBytes)
  {
    numberOfBytes = 0;
    ulong output = 0;
    bool lastByte = false;

    // Read each byte
    while (!lastByte)
    {
      numberOfBytes++;

      // Get next byte
      int nextByte = stream.ReadByte();
      if (nextByte == -1) throw new EndOfStreamException();

      // Check if this is the last byte
      byte next = (byte)nextByte;

      // Check if most significant bit is 0
      // This works because XXX... AND 1000... is 0 only and only if the byte's
      //  MSB is 0
      lastByte = (next & Msb) == 0;

      // Shift output and add new bytes
      output <<= 7;

      // Get bytes with most significant bit set to 0. This work because
      //  XXX... AND 0111... is 0XXX...
      output |= (uint)(next & ~Msb);
    }

    return output;
  }
}
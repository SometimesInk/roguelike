using roguelike.roguelike.engine.networking;

namespace roguelike.roguelike.utils.types;

/// <summary>
/// Unique 8-bit identifier.
/// </summary>
public class Uid
{
  public byte Id;

  public static Uid Null = new Uid(0);

  public Uid()
  {
    HandlerNetwork.IsThisServer(out bool isNotNetworked);

    if (isNotNetworked)
    {
      // Not networked
      return;
    }

    // TODO: Ask server for unused Uid
  }

  public Uid(byte id)
  {
    Id = id;
  }
}
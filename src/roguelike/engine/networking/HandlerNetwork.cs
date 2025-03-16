using System.Net.Sockets;
using roguelike.roguelike.engine.networking.packets;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.configuring;
using roguelike.roguelike.utils.resources.configuring.configs;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.networking;

public static class HandlerNetwork
{
  private static NetworkServer? server;
  private static NetworkClient? client;
  private static CancellationTokenSource? cancellationTokenSource;

  public static bool IsThisServer(out bool isNor)
  {
    bool isServer = server != null;

    if (client == null && !isServer)
    {
      isNor = true;
      return false;
    }

    isNor = false;
    return isServer;
  }

  #region Server Hosting Logic

  public static List<TcpClient> GetClients()
  {
    if (server != null) return server.Clients;

    if (client == null)
    {
      Translatable.Log("Not hosting, nor client.");
      throw new ArgumentNullException();
    }

    // TODO: Ask host for clients
    throw new NotImplementedException();
  }

  public static void StartServer(EntryWindow win)
  {
    _ = StartServerAsync(win).ContinueWith(t =>
    {
      if (t.Exception != null)
      {
        Translatable.Log($"Error starting server: {t.Exception}");
      }
    }, TaskContinuationOptions.OnlyOnFaulted);
  }

  private static async Task StartServerAsync(EntryWindow win)
  {
    if (client != null)
    {
      Translatable.Log("Cannot host while being a client.");
      return;
    }

    if (server != null)
    {
      Translatable.Log("Server is already running.");
      return;
    }

    // Initialize server
    ConfigNetworking conf = ConfigHandler.GetConfig<ConfigNetworking>(win);
    server = new NetworkServer(conf.Address, conf.Port);

    // Recreate a CancellationTokenSource as they are single-use
    cancellationTokenSource = new CancellationTokenSource();

    // Start server
    try
    {
      await server.StartAsync(cancellationTokenSource.Token);
    }
    catch (OperationCanceledException)
    {
      Translatable.Log("Operation was canceled.");
    }
    catch (Exception e)
    {
      Translatable.Log($"An error occurred {e.Message}.");
    }
    finally
    {
      cancellationTokenSource.Dispose();
    }
  }

  public static void StopServer()
  {
    // Check if server is running
    if (server == null)
    {
      Translatable.Log("No running server to stop.");
      return;
    }

    cancellationTokenSource?.Cancel();
    cancellationTokenSource?.Dispose();

    // Reset server and token
    server = null;
    cancellationTokenSource = null;
  }

  #endregion

  #region Client Logic

  public static void ConnectToServer(string address, int port)
  {
    _ = ConnectToServerAsync(address, port).ContinueWith(t =>
    {
      if (t.Exception != null)
      {
        Translatable.Log($"Error connecting to server: {t.Exception}");
      }
    }, TaskContinuationOptions.OnlyOnFaulted);
  }

  private static async Task ConnectToServerAsync(string address, int port)
  {
    // Check if server is running
    if (server != null)
    {
      Translatable.Log("Cannot join server while hosting.");
      return;
    }

    if (client != null)
    {
      Translatable.Log("Already connect to a server.");
      return;
    }

    // Initialize client
    client = new NetworkClient();

    // Recreate a CancellationTokenSource as they are single-use
    cancellationTokenSource = new CancellationTokenSource();

    // Start client
    try
    {
      await client.ConnectAsync(address, port, cancellationTokenSource.Token);
    }
    catch (OperationCanceledException)
    {
      Translatable.Log("Operation was canceled.");
    }
    catch (Exception e)
    {
      Translatable.Log($"An error occurred {e.Message}.");
    }
  }

  public static void DisconnectFromServer()
  {
    if (client == null)
    {
      Translatable.Log("Not currently connected.");
      return;
    }

    cancellationTokenSource?.Cancel();
    cancellationTokenSource?.Dispose();

    // Reset client and token
    client = null;
    cancellationTokenSource = null;
  }

  public static void SendPacketToHost(Packet packet)
  {
    if (client == null)
    {
      Translatable.Log("Not currently connected.");
      return;
    }

    if (cancellationTokenSource == null) return;

    _ = client.SendMessageAsync(packet, cancellationTokenSource.Token);
  }

  #endregion

}
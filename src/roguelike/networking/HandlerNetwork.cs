using System.Net.Sockets;
using roguelike.roguelike.config;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.networking;

public static class HandlerNetwork
{
  private static NetworkServer? server;
  private static NetworkClient? client;
  private static CancellationTokenSource? cancellationTokenSource;

  #region Server Hosting Logic

  public static List<TcpClient> GetClients()
  {
    if (server != null) return server.Clients;

    if (client == null)
    {
      Translatable.Log("Not hosting, nor client.");
      throw new ArgumentNullException();
    }

    // Ask host for clients
    throw new NotImplementedException();
  }

  public static void StartServer()
  {
    _ = StartServerAsync().ContinueWith(t =>
    {
      if (t.Exception != null)
      {
        Translatable.Log($"Error starting server: {t.Exception}");
      }
    }, TaskContinuationOptions.OnlyOnFaulted);
  }

  private static async Task StartServerAsync()
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
    ConfigNetworking conf = HandlerConfig.GetConfig<ConfigNetworking>();
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

  #endregion

}
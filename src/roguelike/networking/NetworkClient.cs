using System.Net.Sockets;
using System.Text;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.networking;

public class NetworkClient
{
  private TcpClient client;
  private NetworkStream stream;

  public async Task ConnectAsync(string ipAddress, int port, CancellationToken cancellationToken)
  {
    client = new TcpClient();
    await client.ConnectAsync(ipAddress, port, cancellationToken);
    stream = client.GetStream();

    Translatable.Log("Connected to server.");

    _ = Task.Run(() => ReadMessageAsync(cancellationToken), cancellationToken);
  }

  public async Task SendMessageAsync(string message, CancellationToken cancellationToken)
  {
// Check if connection is alive and ready to write
    if (client.Connected && stream.CanWrite && !cancellationToken.IsCancellationRequested)
    {
      byte[] messageBytes = Encoding.UTF8.GetBytes(message);
      await stream.WriteAsync(messageBytes, cancellationToken);
    }
  }

  private async Task ReadMessageAsync(CancellationToken cancellationToken)
  {
    try
    {
      byte[] buffer = new byte[1024];

      while (!cancellationToken.IsCancellationRequested && client.Connected)
      {
        // Read data into buffer
        await stream.ReadExactlyAsync(buffer, cancellationToken);
        string message = Encoding.UTF8.GetString(buffer);
        Translatable.Log($"Received {message}");
      }
    }
    catch (OperationCanceledException)
    {
      // Handle cancellation gracefully
    }
    catch (Exception e)
    {
      Translatable.Log($"An error occurred: {e.Message}");
    }
    finally
    {
      Translatable.Log("Disconnected");
      client.Close();
    }
  }
}
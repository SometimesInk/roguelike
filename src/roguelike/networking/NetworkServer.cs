using System.Net;
using System.Net.Sockets;
using System.Text;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.networking;

public class NetworkServer(string ipAddress, int port)
{
  private readonly TcpListener listener = new(IPAddress.Parse(ipAddress), port);
  public List<TcpClient> Clients = [];

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    listener.Start();
    Translatable.Log("Server started.");

    while (!cancellationToken.IsCancellationRequested)
    {
      TcpClient tcpClient = await listener.AcceptTcpClientAsync(cancellationToken);
      Clients.Add(tcpClient);
      Translatable.Log("Client connected.");

      _ = HandleClientAsync(tcpClient, cancellationToken);
    }
  }

  private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
  {
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];

    try
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        int bytesRead = await stream.ReadAsync(buffer, cancellationToken);
        if (bytesRead == 0) break; // Client disconnected

        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Translatable.Log($"Received '{message}' from client '{((IPEndPoint)client.Client.RemoteEndPoint)?.Address}'.");

        // Echo message back
        await stream.WriteAsync(buffer, cancellationToken);
      }
    }
    catch (Exception e)
    {
      Translatable.Log($"Error occurred: {e.Message}");
    }
    finally
    {
      Clients.Remove(client);
      client.Close();
      Translatable.Log("Client disconnected");
    }
  }
}
using Microsoft.Extensions.Configuration;
using NetMQ.Sockets;

namespace Business.Technical;

public class SocketService : IAsyncDisposable
{
    private readonly string connectionPath;

    public SocketService(IConfiguration _configuration)
    {
        connectionPath = _configuration["NetMQ:PubSubSocketURL"];
        PublisherSocket = new PublisherSocket();
        PublisherSocket.Options.SendHighWatermark = 1000;
        PublisherSocket.Bind(connectionPath);
    }

    public PublisherSocket PublisherSocket { get; }


    public ValueTask DisposeAsync()
    {
        PublisherSocket.Dispose();
        return new ValueTask();
    }

    public SubscriberSocket GetSubscriberSocket()
    {
        var socket = new SubscriberSocket();

        socket.Connect(connectionPath);
        return socket;
    }
}
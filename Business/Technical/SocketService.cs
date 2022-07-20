using NetMQ.Sockets;

namespace Business.Technical;

public class SocketService : IAsyncDisposable
{
    private readonly string connectionPath = "inproc://pubsub-socket";

    public SocketService()
    {
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
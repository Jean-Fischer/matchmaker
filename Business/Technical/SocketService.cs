using NetMQ.Sockets;

namespace Business.Technical;

public class SocketService : IAsyncDisposable
{
    
    public PublisherSocket PublisherSocket { get;  }

    private readonly string connectionPath = "inproc://inproc-demo";
    
    public SocketService()
    {
        PublisherSocket = new PublisherSocket();
        PublisherSocket.Options.SendHighWatermark = 1000;
        PublisherSocket.Bind(connectionPath);
    }

    public SubscriberSocket GetSubscriberSocket()
    {
        var socket =  new SubscriberSocket();

        socket.Connect(connectionPath);
        return socket;
    }
     

    public  ValueTask DisposeAsync()
    {
        PublisherSocket.Dispose();
        return new ValueTask();
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net;

namespace IdeaPhoneControlLibrary
{
  class NetworkService
  {
    private static ILog logger = LogManager.GetLogger("NetworkService");

    private IDataConsumer myDataConsumer;
    private Socket myServerSocket;
    private int myServerPort;

    internal NetworkService(IDataConsumer dataConsumer, Properties props)
    {
      myServerPort = props.NetworkPort;
      myDataConsumer = dataConsumer;
      myServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      myServerSocket.Bind(new IPEndPoint(IPAddress.Any, myServerPort));
      myServerSocket.Listen(1024);
    }

    internal void Start()
    {
      logger.Debug("Start network service");
      while (true)
      {
        var socket = myServerSocket.Accept();
        AcceptCallback(socket);
      }
    }

    private void AcceptCallback(Socket socket)
    {
      const int fullLength = 1024 * 1024;
      var receivedData = new byte[fullLength];
      var length = socket.Receive(receivedData);
      var k = length;
      while (length > 0)
      {
        length = socket.Receive(receivedData, k, fullLength - k, SocketFlags.None);
        k += length;
      }
      socket.Close();
      myDataConsumer.PutDataAsyncWithTimerDelay(receivedData, k);
    }
  }
}

using System;
using System.IO;
using log4net;

namespace IdeaPhoneControlLibrary
{
  public class IdeaPhoneService
  {
    private static ILog logger = LogManager.GetLogger("IdeaPhoneService");
    private static IdeaPhoneService ourInstance;
    private static SerialPortConnectionService mySerialPortConnectionService;

    private IdeaPhoneService()
    {
    }

    public static void Start()
    {
      var properties = new Properties();
      properties.Load();
      mySerialPortConnectionService = new SerialPortConnectionService();
      mySerialPortConnectionService.Start(properties);
      if (!properties.PlaySample)
      {
        var networkService = new NetworkService(mySerialPortConnectionService, properties);
        networkService.Start();
      }

      TestSend(properties);
      while (Console.ReadLine().Length == 0)
      {
        TestSend(properties);
      }
    }

    private static void TestSend(Properties props)
    {
      foreach (var row in File.ReadAllLines("sample.txt"))
      {
        var bytes = Convert(row, props);
        mySerialPortConnectionService.PutData(bytes, bytes.Length);
      }
    }

    private static byte[] Convert(string msg, Properties props)
    {
      if (msg.Length == 0) return new[] {props.MagicNumber};
      var strings = msg.Split(' ');
      var bytes = new byte[strings.Length + 1];
      for (var i = 0; i < strings.Length; i++)
      {
        bytes[i] = byte.Parse(strings[i]);
      }
      bytes[strings.Length] = props.MagicNumber;
      return bytes;
    }
  }
}

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using log4net;

namespace IdeaPhoneControlLibrary
{
  internal interface IDataConsumer
  {
    void PutDataAsyncWithTimerDelay(byte[] data, int length);
  }

  internal class SerialPortConnectionService : IDataConsumer
  {
    private static readonly ILog logger = LogManager.GetLogger("SerialPortConnectionService");

    private SerialPort mySerialPort;
    private readonly LinkedList<byte[]> myData = new LinkedList<byte[]>();
    private readonly object myDataMonitor = new object();
    private long myInterval;
    private byte myMagicNumber;
    private Timer myTimer;

    public void Start(Properties props)
    {
      myInterval = props.Interval;
      myMagicNumber = props.MagicNumber;
      InitSerialPort(props.ComPort);
      StartTimer();
    }

    private void StartTimer()
    {
      myTimer = new Timer(SendData, null, 0, myInterval);
    }
    
    

    public void PutDataAsyncWithTimerDelay(byte[] data, int length)
    {
      lock (myDataMonitor)
      {
        var last = -1;
        while (last < length)
        {
          var prev = last;
          last = FindNextMagicNumber(data, length, prev + 1);
          if (last == length) break;
          var l = last - prev;
          var step = new byte[l];
          for (var i = 0; i < l; i++)
          {
            step[i] = data[prev + 1 + i];
          }
          myData.AddLast(step);
        }
      }
    }

    
    
    private int FindNextMagicNumber(byte[] data, int length, int offset)
    {
      for (var i = offset; i < length; i++)
      {
        if (data[i] == myMagicNumber)
          return i;
      }
      return length;
    }



    public void PlayNow(byte[] rawBytes)
    {
      logger.Debug($"Playing bytes: '[{string.Join(", ", rawBytes)}]'");
      if (rawBytes.Length == 0)
        return;

      byte[] bytesToPlay;
      if (rawBytes[rawBytes.Length - 1] == myMagicNumber)
      {
        bytesToPlay = rawBytes;
      }
      else
      {
        bytesToPlay = new byte[rawBytes.Length + 1];
        Array.Copy(rawBytes, bytesToPlay, rawBytes.Length);
        bytesToPlay[rawBytes.Length] = myMagicNumber;
      }
     
      
      mySerialPort.Write(bytesToPlay, 0, bytesToPlay.Length);
      LogSentBytes(bytesToPlay);
    }
    
    
    private void SendData(object state)
    {
      byte[] bytes;
      lock (myDataMonitor)
      {
        if (myData.Count == 0) return;
        bytes = myData.First.Value;
        myData.RemoveFirst();
      }
      if (bytes.Length == 0) return;

      mySerialPort.Write(bytes, 0, bytes.Length);
      LogSentBytes(bytes);
    }
  
    
    private static void LogSentBytes(byte[] bytes)
    {
//      if (!logger.IsDebugEnabled) return;
      var sb = new StringBuilder("Send to serial port: ");
      foreach (var b in bytes)
      {
        sb.Append(' ').Append(b);
      }
      logger.Debug(sb.ToString());
    }


    private void InitSerialPort(string comPort)
    {
      var availableSerialPorts = SerialPort.GetPortNames();
      LogAvailableComPorts(availableSerialPorts);
      if (availableSerialPorts.Length == 0)
        throw new Exception("No available serial port");
      var portToConnect = comPort ?? availableSerialPorts[0];
      logger.DebugFormat("Connecting to serial port {0}", portToConnect);
      mySerialPort = new SerialPort(portToConnect);
      mySerialPort.Open();
    }

    private void LogAvailableComPorts(string[] availableComPorts)
    {
      if (!logger.IsDebugEnabled) return;
      var sb = new StringBuilder("Available serial ports: ");
      foreach (var availableComPort in availableComPorts)
      {
        sb.Append(' ').Append(availableComPort);
      }
      logger.Debug(sb.ToString());
    }
  }
}

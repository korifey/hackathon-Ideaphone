using System.Collections.Generic;
using System.IO;

namespace IdeaPhoneControlLibrary
{
  class Properties
  {
    private const string MAGIC_NUMBER_KEY = "MagicNumber";
    private const string NETWORK_PORT_KEY = "NetworkPort";
    private const string INTERVAL_KEY = "IntervalInMs";
    private const string COM_PORT_KEY = "ComPort";
    private const string PLAY_SAMPLE = "PlaySample";

    private IDictionary<string, string> myProperties;

    private byte myMagicNumber = 255;
    private int myNetworkPort = 11433;
    private long myInterval = 50;
    private string myComPort = null;
    public bool PlaySample { get; private set; } = false;

    public void Load(string filename = "config.properties")
    {
      myProperties  = new Dictionary<string, string>();
      foreach (var row in File.ReadAllLines(filename))
      {
        if (row.StartsWith("//")) continue;
        if (row.StartsWith("#")) continue;
        var parts = row.Split('=');
        myProperties.Add(parts[0], parts[1]);
      }

      string s;
      if (myProperties.TryGetValue(INTERVAL_KEY, out s))
        myInterval = long.Parse(s);

      if (myProperties.TryGetValue(NETWORK_PORT_KEY, out s))
        myNetworkPort = int.Parse(s);

      if (myProperties.TryGetValue(MAGIC_NUMBER_KEY, out s))
        myMagicNumber = byte.Parse(s);

      if (myProperties.TryGetValue(COM_PORT_KEY, out s))
        myComPort = s;
      
      if (myProperties.TryGetValue(PLAY_SAMPLE, out s))
        PlaySample = bool.Parse(s);
    }

    public byte MagicNumber
    {
      get { return myMagicNumber; }
    }

    public int NetworkPort
    {
      get { return myNetworkPort; }
    }

    public long Interval
    {
      get { return myInterval; }
    }

    public string ComPort
    {
      get { return myComPort; }
    }
  }
}

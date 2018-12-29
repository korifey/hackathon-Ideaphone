using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;

namespace IdeaPhoneControlLibrary
{
  public class IdeaPhoneService
  {
    private static ILog _logger = LogManager.GetLogger("IdeaPhoneService");
    private static IdeaPhoneService ourInstance;
    private static SerialPortConnectionService mySerialPortConnectionService;


    
    private IdeaPhoneService()
    {
    }

    public static void Start(string[] files)
    {
      var properties = new Properties();
      properties.Load();
      mySerialPortConnectionService = new SerialPortConnectionService();
      mySerialPortConnectionService.Start(properties);


      //files
      if (files != null && files.Length > 0)
      {
        _logger.Debug($"There are {files.Length} files to play");
        foreach (var f in files)
          PlayFile(f);
      }
      
      //network
      new Thread(() =>
      {
        new NetworkService(mySerialPortConnectionService, properties).Start();
      }).Start(); 

      //console
      while (true)
      {
        var nextLine = Console.ReadLine();
        
        if (string.IsNullOrEmpty(nextLine))
          continue;
          
        if (nextLine == "exit")
          Environment.Exit(0);

        if (nextLine.All(Char.IsDigit))
        {
          byte note = byte.Parse(nextLine);
          _logger.Debug($"Note with code `{note}` received");
          mySerialPortConnectionService.PlayNow(new[]{note});
        }
        else
        {
          PlayFile(nextLine);
        }
      }
      
    }

    private static void PlayFile(string file)
    {
      int[] NotesFromC =
      {
        0, //C
        2, //D
        4, //E
        5, //F
        7, //G
        9, //A
        11, //B
      };
      int shiftForC = 3;

      char[] dies =
      {
        'F', 'C', 'G', 'D', 'A', 'E', 'B'
      };
      char[] bemoles =
      {
        'B', 'E', 'A', 'D', 'G', 'C', 'F'
      };
      
      
      var bpm = 120;
      var key = 0; // 0 = C-dur, -1 = 1b (F-dur), 1 = 1# (G-dur)
      
      string[] lines;
      try
      {
        lines = File.ReadAllLines(file);


      foreach (var line in lines)
      {
        if (string.IsNullOrEmpty(line))
          continue;
        
        if (line.StartsWith("##"))
        {
          var infos = line.Substring(2).Split(',');
          foreach (var infoX in infos)
          {
            var info = infoX.Trim();
            if (Regex.IsMatch(info, "(\\d+)=(\\d+)"))
            {
              var aa = info.Split('=');
              var left = int.Parse(aa[0]);
              var right = int.Parse(aa[1]);
              bpm = left / 4 * right;
              
            } else if (info.Length == 2 && Char.IsDigit(info[0]) && (info[1] == 'b' || info[1] == '#'))
            {
              key = (info[0] - '0') * (info[1] == '#' ? 1 : -1);
            }
            else
            {
              throw new Exception($"Unknown meta information: `{info}`");
            }
          }
          
          continue;
          
        } else if (line.StartsWith("#") || string.IsNullOrEmpty(line))
          continue;

        
        var curRawBytes = new List<byte>(); 
        //read notes
        foreach (var x in line.Split(' ').Where(it => !string.IsNullOrEmpty(it)))
        {
          _logger.Debug("Read: "+x);
          
          int ReadNote(string note, bool bekar)
          {
            note = note.ToUpper();
            var n = note[0];
            if (n < 'A' || n > 'G')
              throw new Exception("Illegal note: "+note);

            int noteIndex = (n >= 'C') ? n - 'C' : n + 7 - 'C';
            int res = NotesFromC[noteIndex] + shiftForC;

            if (note.Length > 1) //no bemoles or octeve numbers
            {
              int i = 1;
              if (note[1] == 'B' || note[1] == '#')
              {
                if (note[1] == 'B')
                  res--;
                else
                  res++;
                
                i++;
              }

              if (i < note.Length)
              {
                if (!char.IsDigit(note[i]))
                  throw new Exception("Illegal note: "+note);
                
                var octave = note[i] - '0';
                res += (octave - 1) * 12;
                
                if (++i < note.Length)
                  throw new Exception("Illegal note: "+note);
              }
            }

            if (!bekar)
            {
              
              if (key > 0 && Array.IndexOf(dies, n) < key)
              {
                res++;
              }

              if (key < 0 && Array.IndexOf(bemoles, n) < -key)
              {
                res--;
              }
            }

            return res;
          }


          void AddFiltered(int note)
          {
            if (note < 0 || note > 23)
              return;

            curRawBytes.Add((byte)note);
          }
          
          if (x[0] == '!')
            AddFiltered(ReadNote(x.Substring(1), true));
          
          else if (Char.IsLetter(x[0]))
            AddFiltered(ReadNote(x, false));
          
          else
          {
            var ss = x.Split('/');
            if (ss.Length != 2)
            {
              throw new Exception("Illegal continuity: "+x);
            }

            double lenOfPauseInPartsOfWhole = double.Parse(ss[0]) / double.Parse(ss[1]);
            var waitInMs = (int) (1000 /*ms in sec*/ * 60 /*sec in min*/ *
                           lenOfPauseInPartsOfWhole * 4 /*quoter in whole*/ / /*beats per min in quoter*/ bpm);
            
            mySerialPortConnectionService.PlayNow(curRawBytes.ToArray());
            Thread.Sleep(waitInMs);
            
            curRawBytes.Clear();
          }
          
        }
      }
      
      }
      catch (Exception e)
      {
        Console.WriteLine($"Error parsing file `{file}`");
        return;
      }
    }


    private static void TestSend(Properties props)
    {
      foreach (var row in File.ReadAllLines("sample.txt"))
      {
        var bytes = Convert(row, props);
        mySerialPortConnectionService.PutDataAsyncWithTimerDelay(bytes, bytes.Length);
      }
    }

    private static byte[] Convert(string msg, Properties props)
    {
      if (msg.Length == 0) return new[] { props.MagicNumber };
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

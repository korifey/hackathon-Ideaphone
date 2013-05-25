using System;
using System.Threading;
using IdeaPhoneControlLibrary;
using log4net;
using log4net.Config;

namespace IdeaPhoneControlServiceLauncher
{
  class IdeaPhoneControlServiceLauncher
  {
    private static ILog logger = LogManager.GetLogger(typeof (IdeaPhoneControlServiceLauncher));

    static void Main(string[] args)
    {
      XmlConfigurator.Configure(new System.IO.FileInfo("config.log4net"));
      logger.Debug("Start app");
      IdeaPhoneService.Start();
      Console.ReadKey();
      /*while (true)
      {
        IdeaPhoneService.TestSend();  
        Thread.Sleep(1000);
      }*/

    }
  }
}

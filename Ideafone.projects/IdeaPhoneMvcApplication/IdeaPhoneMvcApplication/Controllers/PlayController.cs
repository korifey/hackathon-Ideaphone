using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Web.Http;
using System.Web.Mvc;

namespace IdeaPhoneMvcApplication.Controllers
{
    public class PlayController : Controller
    {
//      private const string IDEA_PHONE_HOST = "172.20.208.230";
      private const string IDEA_PHONE_HOST = "unit-470.labs.intellij.net";
      private const int IDEA_PHONE_PORT = 11433;
      private const byte MAGIC_NUMBER = 255;
        //
        // GET: /Play/
      private static Dictionary<string, byte> mappings = new Dictionary<string, byte>()
        {
          {"tone-1A", 0},
          {"tone-1As", 1},
          {"tone-1B", 2},
          {"tone-2C", 3},
          {"tone-2Cs", 4},
          {"tone-2D", 5},
          {"tone-2Ds", 6},
          {"tone-2E", 7},
          {"tone-2F", 8},
          {"tone-2Fs", 9},
          {"tone-2G", 10},
          {"tone-2Gs", 11},
          {"tone-2A", 12},
          {"tone-2As", 13},
          {"tone-2B", 14},
          {"tone-3C", 15},
          {"tone-3Cs", 16},
          {"tone-3D", 17},
          {"tone-3Ds", 18},
          {"tone-3E", 19},
          {"tone-3F", 20},
          {"tone-3Fs", 21},
          {"tone-3G", 22},
          {"tone-3Gs", 23}
        };
        public ActionResult Index([FromUri] object data)
        {
          var keys = Request.Params.AllKeys;
           var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(IDEA_PHONE_HOST, IDEA_PHONE_PORT);
          foreach (var key in keys)
          {
            if (key.StartsWith("data"))
            {
              var bytes = Convert(Request.Params[key]);
              socket.Send(bytes);
            }
          }
          socket.Close();  
          return View();
        }

      private byte[] Convert(String param)
      {
        var bytes = new List<byte>();
        var values = param.Split(new char[] {','});
        foreach (var value in values)
        {
          byte code;
          if (mappings.TryGetValue(value, out code))
          {
            bytes.Add(code);
          }
        }
        bytes.Add(MAGIC_NUMBER);
        return bytes.ToArray();
      }
      
      /*  //
        // GET: /Play/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Play/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Play/Create

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Play/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Play/Edit/5

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Play/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Play/Delete/5

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}

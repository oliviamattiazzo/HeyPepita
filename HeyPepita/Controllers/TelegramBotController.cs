using HeyPepita.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace HeyPepita.Controllers
{
   public class TelegramBotController
   {
      public static void ProcessMessages()
      {
         TelegramUpdatesController updController = new TelegramUpdatesController();
         TelegramMessagesSenderController messageSenderController = new TelegramMessagesSenderController();

         TelegramUpdate latestUpdates = updController.GetUpdates();

         if (!latestUpdates.Ok)
            throw new Exception("Error!");

         if (latestUpdates.Updates.Count() <= 0)
            throw new Exception("Error!");

         messageSenderController.ReturnMessages(FilterNewMessages(latestUpdates.Updates));
      }

      public static List<Update> FilterNewMessages(List<Update> lstUpdates)
      {
         return lstUpdates.Where(p => p.MessageData.MessageId > GetLastMessageId()).ToList();
      }

      public static long GetLastMessageId()
      {
         var xml = XDocument.Load(Properties.Resources.ADDRESS_UPDATECONTROLS);
         return long.Parse(xml.Root.Element("LastMessageId").Value);
      }

      public static string GetBotToken()
      {
         var xml = XDocument.Load(Properties.Resources.ADDRESS_KEYS);
         return xml.Root.Element("BotKey").Value;
      }
   }
}

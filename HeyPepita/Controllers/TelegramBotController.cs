﻿using HeyPepita.Models;
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

         messageSenderController.ReturnMessages(FilterNewMessages(latestUpdates.Updates));
      }

      public static List<Update> FilterNewMessages(List<Update> lstUpdates)
      {
         return (List<Update>)lstUpdates.Where(p => p.UpdateId > GetLastUpdateId());
      }

      private static long GetLastUpdateId()
      {
         var xml = XDocument.Load(Properties.Resources.ADDRESS_UPDATECONTROLS);
         return long.Parse(xml.Root.Element("LastUpdateId").Value);
      }
   }
}

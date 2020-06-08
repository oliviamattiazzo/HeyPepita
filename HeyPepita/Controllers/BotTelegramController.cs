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
   public class BotTelegramController
   {
      private static string GetBotToken()
      {
         var xml = XDocument.Load(Properties.Resources.ADDRESS_KEYS);
         return xml.Root.Element("BotKey").Value;
      }

      public List<TelegramUpdate> GetSolicitacoesTelegram()
      {
         string URL = "https://api.telegram.org/bot" + GetBotToken() + "/getUpdates";

         var getUpdates = WebRequest.Create(URL) as HttpWebRequest;
         getUpdates.Method = "GET";

         object responseItems = new object();
         try
         {
            string respBody = null;
            using (var resp = getUpdates.GetResponse().GetResponseStream())
            {
               var respR = new StreamReader(resp);
               respBody = respR.ReadToEnd();
            }

            responseItems = new JavaScriptSerializer().Deserialize<object>(respBody);
         }
         catch
         {
            throw new Exception("Error!");
         }

         return AdjustResponse(responseItems);
      }

      private List<TelegramUpdate> AdjustResponse(object items)
      {
         List<TelegramUpdate> lstUpdates = new List<TelegramUpdate>();

         IList lstItems = (IList)items;
         foreach (dynamic item in lstItems)
         {
            lstUpdates.Add(new TelegramUpdate {
               Ok = bool.Parse(item["ok"]),
               UpdateId = long.Parse(item["update_id"])

            });
         }

         return lstUpdates;
      }
   }
}

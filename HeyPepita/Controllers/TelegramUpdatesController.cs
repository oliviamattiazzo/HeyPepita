using HeyPepita.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace HeyPepita.Controllers
{
   public class TelegramUpdatesController
   {
      public TelegramUpdate GetUpdates()
      {
         string URL = "https://api.telegram.org/bot" + TelegramBotController.GetBotToken() + "/getUpdates";

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
            throw new Exception("Error getting updates from Telegram!");
         }

         return AdjustResponse(responseItems);
      }

      private TelegramUpdate AdjustResponse(object items)
      {
         TelegramUpdate telegramUpdate = new TelegramUpdate();
         IDictionary<string, dynamic> dictionaryItems = (IDictionary<string, dynamic>)items;

         telegramUpdate.Ok = bool.Parse(dictionaryItems["ok"].ToString());

         IList lstResults = (IList)dictionaryItems["result"];
         foreach (dynamic result in lstResults)
         {
            IDictionary<string, dynamic> messageItens = (IDictionary<string, dynamic>)result["message"];
            IDictionary<string, dynamic> messageFrom = (IDictionary<string, dynamic>)messageItens["from"];
            IDictionary<string, dynamic> messageChat = (IDictionary<string, dynamic>)messageItens["chat"];
            Update updateToBeAdded = new Update { UpdateId = long.Parse(result["update_id"].ToString()) };

            updateToBeAdded.MessageData = new Message();
            updateToBeAdded.MessageData.ChatId = messageChat.ContainsKey("id") ? long.Parse(messageChat["id"].ToString()) : 0;
            updateToBeAdded.MessageData.DateMessage = messageItens.ContainsKey("date") ? DateTimeParser(double.Parse(messageItens["date"].ToString())) : DateTime.MinValue;
            updateToBeAdded.MessageData.FirstName = messageFrom.ContainsKey("first_name") ? messageFrom["first_name"].ToString() : string.Empty;
            updateToBeAdded.MessageData.LastName = messageFrom.ContainsKey("last_name") ? messageFrom["last_name"].ToString() : string.Empty;
            updateToBeAdded.MessageData.MessageId = messageItens.ContainsKey("message_id") ? long.Parse(messageItens["message_id"].ToString()) : 0;
            updateToBeAdded.MessageData.Text = messageItens.ContainsKey("text") ? messageItens["text"].ToString() : string.Empty;
            updateToBeAdded.MessageData.UserId = messageFrom.ContainsKey("id") ? long.Parse(messageFrom["id"].ToString()) : 0;
            updateToBeAdded.MessageData.Username = messageFrom.ContainsKey("username") ? messageFrom["username"].ToString() : string.Empty;

            telegramUpdate.Updates.Add(updateToBeAdded);
         }

         return telegramUpdate;
      }

      private DateTime DateTimeParser(double unixTimeStamp)
      {
         //Date time in Unix
         DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
         return dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
      }
   }
}

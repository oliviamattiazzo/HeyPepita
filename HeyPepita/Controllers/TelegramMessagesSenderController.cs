using HeyPepita.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HeyPepita.Controllers
{
   public class TelegramMessagesSenderController
   {
      public void ReturnMessages(IEnumerable<Update> newMessages)
      {
         foreach (Update message in newMessages)
         {
            if (message.MessageData.Text.ToUpper().Contains("BOM DIA"))
               ReplyBomDiaMessage(message.MessageData.ChatId);
            else if (message.MessageData.Text.ToUpper().Contains("NOVIDADES"))
               ReplyBomDiaMessage(message.MessageData.ChatId);
            else if (message.MessageData.Text.ToUpper().Contains("CREDITOS"))
               ReplyBomDiaMessage(message.MessageData.ChatId);
            else
               ReplyDefaultMessage();

            SaveIdLastMessageReplied();
         }
      }

      private void ReplyBomDiaMessage(long chatId)
      {
         string URL = "https://api.telegram.org/bot" + TelegramBotController.GetBotToken() + 
                      "/sendMessage?chat_id=" + chatId + "&text=" + FormataResposta(TweetsArchiveController.GetTweetBomDia());

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
      }

      private string FormataResposta(Tweet tweet)
      {
         return "";
      }

      private void ReplyNovidadesMessage()
      {
         throw new NotImplementedException();
      }

      private void ReplyCreditosMessage()
      {
         throw new NotImplementedException();
      }

      private void ReplyDefaultMessage()
      {
         throw new NotImplementedException();
      }

      private void SaveIdLastMessageReplied()
      {
         throw new NotImplementedException();
      }
   }
}

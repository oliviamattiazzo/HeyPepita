using HeyPepita.Models;
using System;
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
   public class TelegramMessagesSenderController
   {
      public void ReturnMessages(List<Update> newMessages)
      {
         foreach (Update message in newMessages)
         {
            if (message.MessageData.ChatId == 0)
               throw new Exception("Error! ChatId cannot be zero.");

            if (message.MessageData.Text.ToUpper().Contains("BOM DIA"))
               ReplyBomDiaMessage(message.MessageData.ChatId);
            else if (message.MessageData.Text.ToUpper().Contains("NOVIDADES"))
               ReplyNovidadesMessage(message.MessageData.ChatId);
            else if (message.MessageData.Text.ToUpper().Contains("CREDITOS") || message.MessageData.Text.ToUpper().Contains("CRÉDITOS"))
               ReplyCreditosMessage(message.MessageData.ChatId);
            else if (message.MessageData.Text.ToUpper().Contains("/START"))
               ReplyWelcomeMessage(message.MessageData.ChatId);
            else
               ReplyDefaultMessage(message.MessageData.ChatId);

            SaveIdLastMessageReplied(message.MessageData.MessageId);
         }
      }
      
      private void ReplyMessage(long chatId, string mensagem)
      {
         string URL = "https://api.telegram.org/bot" + TelegramBotController.GetBotToken() +
                      "/sendMessage?chat_id=" + chatId + "&text=" + mensagem;

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
            throw new Exception("Error sending the message! chatId: " + chatId);
         }
      }

      private void ReplyBomDiaMessage(long chatId)
      {
         ReplyMessage(chatId, FormataRespostaTweet(TweetsArchiveController.GetTweetBomDia()));
      }

      private string FormataRespostaTweet(Tweet tweet)
      {
         return tweet.NomeUsuario + " - @" + tweet.Username + "\n" +
                tweet.TweetUrl;
      }

      private void ReplyNovidadesMessage(long chatId)
      {
         ReplyMessage(chatId, FormataRespostaTweet(TweetsArchiveController.GetTweetNovidades()));
      }

      private void ReplyCreditosMessage(long chatId)
      {
         ReplyMessage(chatId, Properties.Resources.Resposta_MsgCreditos);
      }

      private void ReplyDefaultMessage(long chatId)
      {
         ReplyMessage(chatId, Properties.Resources.Resposta_MsgPadrao);
      }

      private void ReplyWelcomeMessage(long chatId)
      {
         ReplyMessage(chatId, Properties.Resources.Resposta_MsgWelcome);
      }

      private void SaveIdLastMessageReplied(long messageId)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_UPDATECONTROLS);
         long lastIdMessageSaved = TelegramBotController.GetLastMessageId();

         if (messageId < lastIdMessageSaved)
            throw new Exception("Error! The messageId cannot be less than the last message ID saved!");

         xmlDoc.Root.Element("LastMessageId").Remove();

         XElement root = xmlDoc.Element("Controls");
         root.Add(new XElement("LastMessageId", messageId));
         xmlDoc.Save(Properties.Resources.ADDRESS_UPDATECONTROLS);
      }
   }
}

using HeyPepita.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HeyPepita.Controllers
{
   public class TwitterApiController
   {
      public static List<Tweet> GetTweets(int qtd)
      {
         string accessToken = AuthorizationController.GetAccessToken();

         var getTimeline = WebRequest.Create(@"https://api.twitter.com/1.1/statuses/user_timeline.json?" +
                                              "screen_name=pepitaofc&" +
                                              "count=" + qtd + "&" +
                                              "include_rts=false" +
                                              "&tweet_mode=extended") as HttpWebRequest;
         getTimeline.Method = "GET";
         getTimeline.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;

         object responseItems = new object();
         try
         {
            string respBody = null;
            using (var resp = getTimeline.GetResponse().GetResponseStream())
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

      private static List<Tweet> AdjustResponse(object items)
      {
         List<Tweet> lstTweets = new List<Tweet>();

         IList lstItems = (IList)items;
         foreach(dynamic item in lstItems)
         {
            lstTweets.Add(new Tweet {
               CreatedAt = DateTimeParser(item["created_at"]),
               FullText = item["full_text"],
               Id = item["id"].ToString()
            });
         }

         return lstTweets;
      }

      private static DateTime DateTimeParser(string dateString)
      {
         //Date time format example: Thu Oct 17 20:06:45 + 0000 2019
         return DateTime.ParseExact("Tue Dec 01 22:35:28 +0000 2015", "ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
      }
   }
}

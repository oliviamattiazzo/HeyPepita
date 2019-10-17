using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Xml;

namespace HeyPepita.Controllers
{
   public class AuthorizationController
   {
      private const string ADDRESS_KEYS = @"..\..\AuthorizationKeys.xml";
      private static string GetConsumerKey()
      {
         var xml = XDocument.Load(ADDRESS_KEYS);
         return xml.Root.Elements("ConsumerKey").First().Value;
      }

      private static string GetConsumerSecret()
      {
         var xml = XDocument.Load(ADDRESS_KEYS);
         return xml.Root.Elements("ConsumerSecret").First().Value;
      }

      //Credits: https://www.codeproject.com/Tips/1076400/Twitter-API-for-beginners
      public static string GetAccessToken()
      {
         string accessToken = "";
         string consumerKey = GetConsumerKey();
         string consumerSecret = GetConsumerSecret();
         var credentials = Convert.ToBase64String(new UTF8Encoding().GetBytes(GetConsumerKey() + ":" + GetConsumerSecret()));

         var request = WebRequest.Create("https://api.twitter.com/oauth2/token") as HttpWebRequest;
         request.Method = "POST";
         request.ContentType = "application/x-www-form-urlencoded";
         request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

         var reqBody = Encoding.UTF8.GetBytes("grant_type=client_credentials");
         request.ContentLength = reqBody.Length;

         using (var req = request.GetRequestStream())
         {
            req.Write(reqBody, 0, reqBody.Length);
         }
         try
         {
            string respbody = null;
            using (var resp = request.GetResponse().GetResponseStream())
            {
               var respR = new StreamReader(resp);
               respbody = respR.ReadToEnd();
            }

            accessToken = respbody.Substring(respbody.IndexOf("access_token\":\"") + "access_token\":\"".Length, respbody.IndexOf("\"}") - (respbody.IndexOf("access_token\":\"") + "access_token\":\"".Length));
         }
         catch
         {
            throw new Exception("Error!");
         }

         return accessToken;
      }
   }
}

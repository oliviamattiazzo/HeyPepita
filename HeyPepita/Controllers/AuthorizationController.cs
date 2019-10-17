using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HeyPepita.Controllers
{
   public class AuthorizationController
   {
      private const string ADDRESS_KEYS = @"..\AuthorizationKeys.xml";
      public static string GetConsumerKey()
      {
         var xml = XDocument.Load(ADDRESS_KEYS);
         return xml.Root.Elements().Select(x => x.Element("ConsumerKey")).First().Value;
      }

      public static string GetConsumerSecret()
      {
         var xml = XDocument.Load(ADDRESS_KEYS);
         return xml.Root.Elements().Select(x => x.Element("ConsumerSecret")).First().Value;
      }

      public async Task<string> GetAccessToken()
      {
         string consumerKey =

         HttpClient client = new HttpClient();
         var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
         var credentials = Convert.ToBase64String(new UTF8Encoding().GetBytes())
      }
   }
}

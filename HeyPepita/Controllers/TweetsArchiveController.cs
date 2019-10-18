using HeyPepita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HeyPepita.Controllers
{
   public class TweetsArchiveController
   {
      private const string ADDRESS_TWEETS = @"..\..\Tweets.xml";

      public static void SaveLastTweet(Tweet lastTweet)
      {
         XDocument xmlDoc = XDocument.Load(ADDRESS_TWEETS);
         bool tweetIsNewer = true;

         if (!string.IsNullOrEmpty(xmlDoc.Root.Element("LastTweet").Value))
            tweetIsNewer = CheckCreationDateBetweenTweets(lastTweet);

         if (tweetIsNewer)
         {
            XElement root = xmlDoc.Element("Tweets");
            IEnumerable<XElement> rows = root.Descendants("LastTweet");
            XElement firstRow = rows.First();
            firstRow.Add(new XElement("CreatedAt", lastTweet.CreatedAt),
                         new XElement("Id", lastTweet.Id),
                         new XElement("FullText", lastTweet.FullText));
            xmlDoc.Save(ADDRESS_TWEETS);
         }
      }

      public static bool CheckCreationDateBetweenTweets(Tweet lastTweet)
      {
         return false;
      }
   }
}

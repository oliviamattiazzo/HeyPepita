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
      public static void SaveLastTweet(Tweet lastTweet)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         bool tweetIsNewer = true;

         if (TweetArchive.GetLastTweetFromArchive() != null)
            tweetIsNewer = CheckCreationDateBetweenTweets(lastTweet);

         if (tweetIsNewer)
         {
            XElement root = xmlDoc.Element("Tweets");
            IEnumerable<XElement> rows = root.Descendants("LastTweet");
            XElement firstRow = rows.First();
            firstRow.Add(new XElement("CreatedAt", lastTweet.CreatedAt),
                         new XElement("Id", lastTweet.Id),
                         new XElement("FullText", lastTweet.FullText));
            xmlDoc.Save(Properties.Resources.ADDRESS_TWEETS);
         }
      }

      public static bool CheckCreationDateBetweenTweets(Tweet lastTweet)
      {
         Tweet lastTwtFromArchive = TweetArchive.GetLastTweetFromArchive();

         if (lastTweet.Id == lastTwtFromArchive.Id)
            return false;

         if (DateTime.Compare(lastTweet.CreatedAt, lastTwtFromArchive.CreatedAt) >= 0)
            return false;

         return true;
      }
   }
}

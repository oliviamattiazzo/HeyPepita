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
      public static void Saves(List<Tweet> lstTweets)
      {
         SaveLastTweet(lstTweets.First());
         SaveLastGoodMorningTweet(TweetAnalyzerController.FindGoodMorningTweet(lstTweets));
      }

      public static void SaveLastTweet(Tweet lastTweet)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         bool tweetIsNewer = true;

         if (TweetArchive.GetLastTweetFromArchive() != null)
            tweetIsNewer = CheckCreationDateBetweenTweets(lastTweet, TweetArchive.GetLastTweetFromArchive());

         if (tweetIsNewer)
         {
            DeleteTweet(xmlDoc, "LastTweet");

            XElement root = xmlDoc.Element("Tweets");
            IEnumerable<XElement> rows = root.Descendants("LastTweet");
            XElement firstRow = rows.First();
            firstRow.Add(new XElement("CreatedAt", lastTweet.CreatedAt),
                         new XElement("Id", lastTweet.Id),
                         new XElement("FullText", lastTweet.FullText));
            xmlDoc.Save(Properties.Resources.ADDRESS_TWEETS);
         }
      }

      public static bool CheckCreationDateBetweenTweets(Tweet lastTweet, Tweet tweetFromArchive)
      {
         if (lastTweet.Id == tweetFromArchive.Id)
            return false;

         if (DateTime.Compare(lastTweet.CreatedAt, tweetFromArchive.CreatedAt) <= 0)
            return false;

         return true;
      }

      public static void SaveLastGoodMorningTweet(Tweet lastGmTweet)
      {
         if (lastGmTweet == null)
            throw new Exception("Error!");

         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         bool tweetIsNewer = true;

         if (TweetArchive.GetLastGoodMorningTweetFromArchive() != null)
            tweetIsNewer = CheckCreationDateBetweenTweets(lastGmTweet, TweetArchive.GetLastGoodMorningTweetFromArchive());

         if (tweetIsNewer)
         {
            DeleteTweet(xmlDoc, "LastGoodMorningTweet");

            XElement root = xmlDoc.Element("Tweets");
            IEnumerable<XElement> rows = root.Descendants("LastGoodMorningTweet");
            XElement firstRow = rows.First();
            firstRow.Add(new XElement("CreatedAt", lastGmTweet.CreatedAt),
                         new XElement("Id", lastGmTweet.Id),
                         new XElement("FullText", lastGmTweet.FullText));
            xmlDoc.Save(Properties.Resources.ADDRESS_TWEETS);
         }
      }

      public static void DeleteTweet(XDocument xmlDoc, string tweetIdentifier)
      {
         xmlDoc.Root.Element(tweetIdentifier).Elements().ToList().Remove();
      }
   }
}

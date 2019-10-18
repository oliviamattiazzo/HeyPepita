using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HeyPepita.Models
{
   public class TweetArchive : Tweet
   {
      public static DateTime GetDateTimeFromArchive(string tweetIdentifier)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return DateTime.Parse(xmlDoc.Root.Element(tweetIdentifier).Element("CreatedAt").Value);
      }

      public static string GetIdFromArchive(string tweetIdentifier)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return xmlDoc.Root.Element(tweetIdentifier).Element("Id").Value;
      }

      public static string GetFullTextFromArchive(string tweetIdentifier)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return xmlDoc.Root.Element(tweetIdentifier).Element("FullText").Value;
      }

      public static Tweet GetLastTweetFromArchive()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);

         if (string.IsNullOrEmpty(xmlDoc.Root.Element("LastTweet").Value))
            return null;

         XElement lastTweet = xmlDoc.Root.Element("LastTweet");
         return new Tweet {
            CreatedAt = DateTime.Parse(lastTweet.Element("CreatedAt").Value),
            FullText = lastTweet.Element("FullText").Value,
            Id = lastTweet.Element("Id").Value
         };
      }

      public static Tweet GetLastGoodMorningTweetFromArchive()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);

         if (string.IsNullOrEmpty(xmlDoc.Root.Element("LastGoodMorningTweet").Value))
            return null;

         XElement lastTweet = xmlDoc.Root.Element("LastGoodMorningTweet");
         return new Tweet
         {
            CreatedAt = DateTime.Parse(lastTweet.Element("CreatedAt").Value),
            FullText = lastTweet.Element("FullText").Value,
            Id = lastTweet.Element("Id").Value
         };
      }
   }
}

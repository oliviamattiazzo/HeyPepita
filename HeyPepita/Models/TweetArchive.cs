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
      #region Last Tweet

      public static DateTime GetDateTimeFromLastTweet()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return DateTime.Parse(xmlDoc.Root.Element("LastTweet").Element("CreatedAt").Value);
      }

      public static string GetIdFromLastTweet()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return xmlDoc.Root.Element("LastTweet").Element("Id").Value;
      }

      public static string GetFullTextFromLastTweet()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return xmlDoc.Root.Element("LastTweet").Element("FullText").Value;
      }

      public static Tweet GetLastTweetFromArchive()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);

         if (string.IsNullOrEmpty(xmlDoc.Root.Element("LastTweet").Value))
            return null;

         XElement lastTweet = xmlDoc.Root.Element("LastTweet");
         return new Tweet
         {
            CreatedAt = DateTime.Parse(lastTweet.Element("CreatedAt").Value),
            FullText = lastTweet.Element("FullText").Value,
            Id = lastTweet.Element("Id").Value
         };
      }
      #endregion

      #region Last Good Morning Tweet

      public static DateTime GetDateTimeFromLastGoodMorningTweet()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return DateTime.Parse(xmlDoc.Root.Element("LastGoodMorningTweet").Element("CreatedAt").Value);
      }
      
      public static string GetIdFromLastGoodMorningTweet()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return xmlDoc.Root.Element("LastGoodMorningTweet").Element("Id").Value;
      }
      
      public static string GetFullTextFromLastGoodMorningTweet()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         return xmlDoc.Root.Element("LastGoodMorningTweet").Element("FullText").Value;
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
      #endregion
   }
}

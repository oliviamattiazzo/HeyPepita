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
         if (lstTweets.Count <= 0)
            return;

         SaveUserInfo(lstTweets.First());
         SaveLastTweet(lstTweets.First());
         SaveLastGoodMorningTweet(TweetAnalyzerController.FindGoodMorningTweet(lstTweets));
      }

      private static void SaveUserInfo(Tweet tweet)
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);

         XElement root = xmlDoc.Element("Tweets");
         IEnumerable<XElement> itens = root.Descendants("DadosPepita");

         if (string.IsNullOrEmpty(itens.First().Value) || itens.First().Value != tweet.NomeUsuario)
         {
            xmlDoc.Root.Element("DadosPepita").Element("NomeUsuario").Remove();

            XElement itemNomeUsuario = itens.First();
            itemNomeUsuario.Add(new XElement("NomeUsuario", tweet.NomeUsuario));
         }

         if (string.IsNullOrEmpty(itens.First().Value) || itens.First().Value != tweet.Username)
         {
            xmlDoc.Root.Element("DadosPepita").Element("Username").Remove();

            XElement itemNomeUsuario = itens.First();
            itemNomeUsuario.Add(new XElement("Username", tweet.Username));
         }

         xmlDoc.Save(Properties.Resources.ADDRESS_TWEETS);
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
                         new XElement("FullText", lastTweet.FullText),
                         new XElement("TweetUrl", lastTweet.TweetUrl));
            xmlDoc.Save(Properties.Resources.ADDRESS_TWEETS);
         }
      }

      public static Tweet GetTweetNovidades()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         XElement latestTweet = xmlDoc.Element("Tweets").Element("LastTweet");
         XElement userInfo = xmlDoc.Element("Tweets").Element("DadosPepita");

         return new Tweet
         {
            CreatedAt = DateTime.Parse(latestTweet.Element("CreatedAt").Value),
            FullText = latestTweet.Element("FullText").Value,
            Id = latestTweet.Element("Id").Value,
            NomeUsuario = userInfo.Element("NomeUsuario").Value,
            TweetUrl = latestTweet.Element("TweetUrl").Value,
            Username = userInfo.Element("Username").Value
         };
      }

      public static Tweet GetTweetBomDia()
      {
         XDocument xmlDoc = XDocument.Load(Properties.Resources.ADDRESS_TWEETS);
         XElement goodMorningTweet = xmlDoc.Element("Tweets").Element("LastGoodMorningTweet");
         XElement userInfo = xmlDoc.Element("Tweets").Element("DadosPepita");

         return new Tweet
         {
            CreatedAt = DateTime.Parse(goodMorningTweet.Element("CreatedAt").Value),
            FullText = goodMorningTweet.Element("FullText").Value,
            Id = goodMorningTweet.Element("Id").Value,
            NomeUsuario = userInfo.Element("NomeUsuario").Value,
            TweetUrl = goodMorningTweet.Element("TweetUrl").Value,
            Username = userInfo.Element("Username").Value
         };
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
                         new XElement("FullText", lastGmTweet.FullText),
                         new XElement("TweetUrl", lastGmTweet.TweetUrl));
            xmlDoc.Save(Properties.Resources.ADDRESS_TWEETS);
         }
      }

      public static void DeleteTweet(XDocument xmlDoc, string tweetIdentifier)
      {
         xmlDoc.Root.Element(tweetIdentifier).Elements().ToList().Remove();
      }
   }
}

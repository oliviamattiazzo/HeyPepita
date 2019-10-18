using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeyPepita.Models;

namespace HeyPepita.Controllers
{
   public class TweetAnalyzerController
   {
      public static Tweet FindGoodMorningTweet(List<Tweet> lstTweets)
      {
         foreach (Tweet twt in lstTweets)
         {
            string fullTextLower = twt.FullText.ToLower();

            int points = GetNumberPointsForTweets(fullTextLower);
            if (points >= 2)
               return twt;
         }

         return null;
      }

      public static int GetNumberPointsForTweets(string fullText)
      {
         string[] goodMorningWords = Properties.Resources.WORDS_GOOD_MORNING.Split(',');
         int totalPoints = 0;
         
         foreach(string word in goodMorningWords)
         {
            if (fullText.Contains(word))
               totalPoints++;
         }

         return totalPoints;
      }
   }
}

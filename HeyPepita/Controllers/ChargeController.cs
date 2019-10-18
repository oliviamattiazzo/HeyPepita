﻿using HeyPepita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using HeyPepita.Controllers;

namespace HeyPepita.Controllers
{
   public class ChargeController
   {
      public static void FirstCharging()
      {
         List<Tweet> lstTweets = TwitterApiController.GetTweets(10);
         lstTweets.OrderBy(p => p.CreatedAt);
         TweetsArchiveController.SaveLastTweet(lstTweets.First());
      }
   }
}

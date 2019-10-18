using HeyPepita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyPepita.Controllers
{
   public class ChargeController
   {
      public static void FirtCharging()
      {
         List<Tweet> lstTweets = TwitterController.GetTweets(10);
      }
   }
}

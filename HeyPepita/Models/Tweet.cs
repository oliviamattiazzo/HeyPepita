using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyPepita.Models
{
   public class Tweet
   {
      public DateTime CreatedAt { get; set; }

      public string Id { get; set; }

      public string FullText { get; set; }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyPepita.Models
{
   public class TelegramUpdate
   {
      public bool Ok { get; set; }

      public long UpdateId { get; set; }

      public Message Message { get; set; }

      public DateTime DateUpdate { get; set; }

      public string Text { get; set; }
   }

   public partial class Message
   {
      protected long MessageId { get; set; }

      public long UserId { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Username { get; set; }
   }
}

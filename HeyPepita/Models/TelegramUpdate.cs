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

      public List<Update> Updates { get; set; }

      public TelegramUpdate()
      {
         Updates = new List<Update>();
      }
   }

   public partial class Update
   {
      public long UpdateId { get; set; }
      
      public Message MessageData { get; set; }

      public Update()
      {
         MessageData = new Message();
      }
   }

   public partial class Message
   {
      public long MessageId { get; set; }

      public long UserId { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Username { get; set; }

      public long ChatId { get; set; }

      public DateTime DateMessage { get; set; }

      public string Text { get; set; }
   }
}

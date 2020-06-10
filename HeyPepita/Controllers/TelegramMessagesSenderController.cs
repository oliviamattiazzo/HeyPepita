using HeyPepita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyPepita.Controllers
{
   public class TelegramMessagesSenderController
   {
      public void ReturnMessages(List<Update> newMessages)
      {
         //pegar cada mensagem e responder
            //se "bom dia" -> ultimo tweet de bom dia
            //se "novidades" -> ultimo tweet total
            //se "creditos" -> FALAR DE EUZINHA
         //guardar updateid da ultima mensagem respondida
      }
   }
}

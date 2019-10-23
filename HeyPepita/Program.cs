using HeyPepita.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeyPepita
{
   class Program
   {
      public static void Main(string[] args)
      {
         int daysControl = 0;
#if RELEASE
         ChargeController.FirstCharging();

         while(true)
         {
            daysControl++;
            Thread.Sleep(TimeSpan.FromHours(1));

            if (daysControl == 24)
            {
               ChargeController.UpdateTweets();
               daysControl = 0;
            }
         }
#endif

         ChargeController.UpdateTweets();

         Console.ReadKey();

         /* ********** TELEGRAM ********** */
         //Setar webhook que vai ficar aguardando a solicitação do bot
         //Fazer o envio da mensagem quando a solicitação chegar


         //TODO
         //1 - Melhorar tratamento de erros
         //2 - Projeto de testes
         //3 - Salvar logs (especialmente relacionado as buscas e ao tempo)
      }
   }
}

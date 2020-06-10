using HeyPepita.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
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

#if DEBUG
         int startingHour = DateTime.Now.Hour;
         int startingMinute = DateTime.Now.AddMinutes(1).Minute;

         MyScheduler.IntervalInMinutes(startingHour, startingMinute, 1, () =>
         {
            ChargeController.UpdateTweets();
         });

         MyScheduler.IntervalInMinutes(startingHour, startingMinute, 1, () =>
         {
            TelegramBotController.ProcessMessages();
         });

         Console.ReadLine();

#endif

#if RELEASE

         ChargeController.FirstCharging();
         
         //Começa às 7h | Intervalo de 1 dia
         MyScheduler.IntervalInDays(7, 00, 1, () =>
         {
            ChargeController.UpdateTweets();
         });

         //Começa às 7h | Intervalo de 1 hora
         MyScheduler.IntervalInHours(7, 00, 1, () =>
         {
            //Programar as coisa do telegram
         });

#endif


         /* ********** TELEGRAM ********** */
         //Fazer o envio da mensagem quando a solicitação chegar


         //TODO
         //1 - Melhorar tratamento de erros
         //2 - Projeto de testes
         //3 - Salvar logs (especialmente relacionado as buscas e ao tempo)
      }
   }
}

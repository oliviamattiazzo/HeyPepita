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
         BotTelegramController botTelegram = new BotTelegramController();
         botTelegram.GetSolicitacoesTelegram();


         //int startingHour = DateTime.Now.Hour;
         //int startingMinute = DateTime.Now.AddMinutes(1).Minute;

         //MyScheduler.IntervalInMinutes(startingHour, startingMinute, 1, () =>
         //{
         //   ChargeController.UpdateTweets();
         //});

         //MyScheduler.IntervalInMinutes(startingHour, startingMinute, 1, () =>
         //{
         //   //Programar as coisa do telegram
         //});

         //Console.ReadLine();

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
         //Setar webhook que vai ficar aguardando a solicitação do bot
         //Fazer o envio da mensagem quando a solicitação chegar


         //TODO
         //1 - Melhorar tratamento de erros
         //2 - Projeto de testes
         //3 - Salvar logs (especialmente relacionado as buscas e ao tempo)
      }
   }
}

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
         ChargeController.FirstCharging();
         Console.ReadKey();

         while(true)
         {
            daysControl++;
            Thread.Sleep(TimeSpan.FromHours(1));
         }

         /* ********** TWITTER ********** */

         //Dia N
         //Verificar se há novos tweets
         //Se houver, verificar se algum deles é de bom dia
         //Se tiver tweet de 'bom dia', faz update no tweet de 'bom dia'
         //Se não tiver, faz update no último tweet AT ALL


         /* ********** TELEGRAM ********** */
         //Setar webhook que vai ficar aguardando a solicitação do bot
         //Fazer o envio da mensagem quando a solicitação chegar


         //TODO
         //1 - Melhorar tratamento de erros
      }
   }
}

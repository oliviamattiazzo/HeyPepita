using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyPepita.Controllers
{
   public class LogsController
   {
      public void Log(string message)
      {
         FileInfo fileLogs = new FileInfo(Properties.Resources.ADDRESS_LOGS);
         
         if (!fileLogs.Exists)
         {

         }
      }
   }
}

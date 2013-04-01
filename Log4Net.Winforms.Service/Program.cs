using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log4Net.Winforms.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            CommandArgumentCollection argumentCollection = new CommandArgumentCollection(args);
            
            if (argumentCollection.GetFirstArgument("console") != null)
            {
                Application.Run(new ServiceTest());
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace BridgeCatalpa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            BackgroundService backgroundService;
            backgroundService = new BackgroundService();

            ServicesToRun = new ServiceBase[] 
			{ 
			    backgroundService
			};
            ServiceBase.Run(ServicesToRun);
        }


    }
}

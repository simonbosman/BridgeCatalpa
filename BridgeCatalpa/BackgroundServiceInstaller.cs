using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Diagnostics;


namespace BridgeCatalpa
{
    [RunInstaller(true)]
    public partial class BackgroundServiceInstaller : System.Configuration.Install.Installer
    {
   
        private ServiceInstaller backgroundServiceInstaller1;
        private ServiceProcessInstaller backgroundServiceProcessInstaller;
        private EventLogInstaller catalpaEventLogInstaller;

        public BackgroundServiceInstaller()
        {

            InitializeComponent();

            catalpaEventLogInstaller = new EventLogInstaller();
            catalpaEventLogInstaller.Source = "Bridge Catalpa";
            catalpaEventLogInstaller.Log = "Catalpa Log";
            Installers.Add(catalpaEventLogInstaller);

            backgroundServiceInstaller1 = new ServiceInstaller();
            backgroundServiceProcessInstaller = new ServiceProcessInstaller();
            
            backgroundServiceProcessInstaller.Account = ServiceAccount.LocalSystem;
            backgroundServiceInstaller1.StartType = ServiceStartMode.Automatic;

            backgroundServiceInstaller1.ServiceName = "BridgeCatalpa";
            Installers.Add(backgroundServiceInstaller1);
            
            AfterInstall += new InstallEventHandler(BackgroundServiceInstaller_AfterInstall);
            
            Installers.Add(backgroundServiceProcessInstaller);
            
        }
        
        private static void BackgroundServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController service;
            service = new ServiceController("BridgeCatalpa");
            service.Start();
        }
    }
       
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Afas.ComConnectors;

namespace BridgeCatalpa
{
    partial class BackgroundService : ServiceBase
    {

        public static UpdateConnector updateConnector;
        public static GetConnector getConnector;
        private static BridgeLog bridgeLog;
        private static BridgeCatalpaReg bridgeCatalpaReg;
        private Watcher watcher;
        
        public BackgroundService()
        {
            InitializeComponent();
        }

        public static void setConnector(string env)
        {
            string pass = bridgeCatalpaReg.getRegValue("password");
            string environment = env;
            string userId = bridgeCatalpaReg.getRegValue("userId");

            if (userId.Length == 0)
                userId = "";

            if (pass.Length == 0)
                pass = "";
            else
                pass = EncryptPassword.Decrypt(pass, "cat777");

            if (environment.Length != 0)
            {
                try
                {
                    updateConnector = new UpdateConnector(environment, userId, pass);
                    getConnector = new GetConnector(environment, userId, pass);
                }
                catch (Exception e)
                { 
                       bridgeLog.writeLog(e.Message + e.InnerException, true);
                }
            }
        }


        protected override void OnStart(string[] args)
        {
            bridgeLog = new BridgeLog();
            watcher = new Watcher();
            bridgeCatalpaReg = new BridgeCatalpaReg();
            bridgeLog.writeLog("Bridge Catalpa service, is gestart", false);
            watcher.watch();
        }

        protected override void OnStop()
        {
            bridgeLog.writeLog("Bridge Catalpa service, is gestopt", false);
        }

    }
}

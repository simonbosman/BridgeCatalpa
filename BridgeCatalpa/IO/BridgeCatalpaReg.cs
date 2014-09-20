using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace BridgeCatalpa
{
    class BridgeCatalpaReg
    {

        private RegistryKey rk;
        private BridgeLog bridgeLog;


        public BridgeCatalpaReg()
        {
            bridgeLog = new BridgeLog();
            try
            {
                rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Catalpa", true);
            }
            catch (Exception e)
            {
               bridgeLog.writeLog("Benodigde registersleutel is niet aanwezig.", true);
            }
        }

        public string getRegValue(string regKey)
        {
            if (rk == null)
                return "";

            string retValue = (string)rk.GetValue(regKey, "Niet gevuld");
            return retValue;
        }

        public bool setRegValue(string regKey, object regValue)
        {
            if (rk == null)
                return false;
            else
            {
                rk.SetValue(regKey, regValue, RegistryValueKind.String);
            }
            return true;
        }

    }
}

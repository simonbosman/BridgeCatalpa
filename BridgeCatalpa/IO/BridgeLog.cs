using System;
using System.Diagnostics;

namespace BridgeCatalpa
{
    class BridgeLog
    {
        private EventLog bridgeLog;

        public BridgeLog()
        {
            bridgeLog = new EventLog();
            bridgeLog.Source = "Bridge Catalpa";
            bridgeLog.Log = "Catalpa Log";
        }

        public void writeLog(String entry, bool error)
        {
            if (error)
                bridgeLog.WriteEntry(entry, EventLogEntryType.Error);
            else
                bridgeLog.WriteEntry(entry);
        }

    }
}

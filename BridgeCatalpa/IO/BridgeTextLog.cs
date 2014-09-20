using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BridgeCatalpa
{
    class BridgeTextLog
    {
        public static void Log(String logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  {0}", logMessage);
            w.WriteLine("-------------------------------");
            w.Flush();
        }
        public static string DumpLog(StreamReader r)
        {
            String line;
            String data;
            data = "";

            while ((line = r.ReadLine()) != null)
            {
                data += line;
            }
            r.Close();

            return data;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace BridgeCatalpa
{
    class Filer
    {

        private StreamReader streamReader;
        public static int counter;
        private BridgeLog bridgeLog;

        public Filer(BridgeLog bridgeLog)
        {
            Filer.counter = 0;
            this.bridgeLog = bridgeLog;
        }

        public string readXML(string path, string name)
        {
            string file = path + "\\" + name;
            Filer.counter = 0;
        Again:
            Thread.Sleep(1000);
            if (Filer.counter != 0)
                this.bridgeLog.writeLog("Loop nummer: " + counter, false);

            Filer.counter++;

            try
            {
                using (streamReader = File.OpenText(path))
                {
                    string s = "";
                    string dataXML = "";
                    while ((s = streamReader.ReadLine()) != null)
                    {
                        dataXML += s;
                    }
                    return dataXML;
                }
            }
            catch (Exception e)
            {
                if (Filer.counter < 5)
                    goto Again;
                else
                    return "";
            }
        }

        public void moveXML(string path, string dest, string name)
        {
            string sfile = path;
            string dfile = dest + "\\" + name;
            File.Move(sfile, dfile);
        }


    }
}

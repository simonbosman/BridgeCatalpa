using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BridgeCatalpa.ComConnectors
{
    class FindConnector
    {
        public FindConnector()
        {
        }

        public string getConnector(string fileName)
        {
            string[] name = fileName.Split(new Char[] { '_' });
            if (name[0].Length > 0)
                return name[0];
            else
                return "";
        }

        public string getEnv(string fileName)
        {
            string[] name = fileName.Split(new Char[] { '_' });
            if (name[1].Length > 0)
                return name[1];
            else
                return "";
        }
    }
}

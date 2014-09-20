using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace BridgeCatalpa
{
    public class Watcher
    {
        private BridgeLog bridgeLog;
        private FileSystemWatcher watcher;
        private BridgeCatalpaReg bridgeCatalpaReg;
        private Filer filer;
        private ComConnectors.FindConnector finder;
        private String dataXml;

        public Watcher()
        {
            bridgeLog = new BridgeLog();
            watcher = new FileSystemWatcher();
            bridgeCatalpaReg = new BridgeCatalpaReg();
            filer = new Filer(bridgeLog);
            finder = new ComConnectors.FindConnector();
            dataXml = "";
        }

        public void watch()
        {

            string path = bridgeCatalpaReg.getRegValue("IN");

            try
            {
                watcher.Path = path;
                watcher.Filter = "*.xml";
                watcher.Created += new FileSystemEventHandler(onChanged);
                watcher.EnableRaisingEvents = false;
                watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size | NotifyFilters.Security;
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception eIN)
            {
                bridgeLog.writeLog(eIN.Message + eIN.Source, true);
            }
        }

        private void onChanged(object source, FileSystemEventArgs e)
        {
            BackgroundService.setConnector(finder.getEnv(e.Name)); 
            
            if ((finder.getConnector(e.Name) == "KnSalesRelationPer") & (bridgeCatalpaReg.getRegValue("CheckDebId").Equals("True")))
            {
                if (BackgroundService.getConnector != null)
                {
                    try
                    {
                        String dataCheck = filer.readXML(e.FullPath, e.Name);
                        ComConnectors.CheckDebID checkDeb = new ComConnectors.CheckDebID("Connector_Debiteuren_Controle",
                            bridgeCatalpaReg.getRegValue("LOG") + "\\temp.xml");
                        checkDeb.setDataCheck(dataCheck);
                        dataXml = checkDeb.getCheckDeb();
                    }
                    catch (Exception oErn)
                    {
                        filer.moveXML(e.FullPath, bridgeCatalpaReg.getRegValue("FOUT"), e.Name);
                        bridgeLog.writeLog("CheckDebID -> " + oErn.Message + oErn.Source, true);
                        return;
                    }
                }
                else
                {
                    bridgeLog.writeLog("Er is geen instantie van de getConnector gemaakt", true);
                    return;
                }

            }
            else
            {
                dataXml = filer.readXML(e.FullPath, e.Name);
            }

            if (BackgroundService.updateConnector != null)
            {

                try
                {
                    string connector = finder.getConnector(e.Name);
                    string result = "";

                    if (connector.Length == 0 || dataXml.Length == 0)
                    {
                        result = null;
                        bridgeLog.writeLog("Watcher.onChanged -> De inhoud van dataXml of connector is leeg", true);
                        filer.moveXML(e.FullPath, bridgeCatalpaReg.getRegValue("FOUT"), e.Name);
                        return;
                    }
                    else
                        result = BackgroundService.updateConnector.Execute(connector, 1, dataXml);

                    if (result != null)
                    {
                        String nameFile = "FileName met fout: " + e.Name + "\r" +
                            "-----------------------------------------------\r";
                        bridgeLog.writeLog(nameFile + result, true);

                        string file = bridgeCatalpaReg.getRegValue("LOG") + "\\CatalpaLog.txt";

                        using (StreamWriter w = File.AppendText(file))
                        {
                            BridgeTextLog.Log(nameFile + result, w);
                            w.Close();
                        }

                        filer.moveXML(e.FullPath, bridgeCatalpaReg.getRegValue("FOUT"), e.Name);
                    }
                    else
                    {
                        filer.moveXML(e.FullPath, bridgeCatalpaReg.getRegValue("OUT"), e.Name);
                    }
                }
                catch (Exception oErn)
                {
                    bridgeLog.writeLog(oErn.Message + oErn.Source, true);
                }
            }
            else
            {
                bridgeLog.writeLog("Er is geen instantie van de updateConnector gemaakt", true);
            }
        }

    }
}

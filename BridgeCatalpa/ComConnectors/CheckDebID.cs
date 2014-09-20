using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BridgeCatalpa.ComConnectors
{
    class CheckDebID
    {

        private String connectorId;
        private String dataCheck;
        private XmlDataDocument doc;
        private String tempFile;

        public CheckDebID(string connectorId, string tempFile)
        {
            doc = new XmlDataDocument();
            setConnectorId(connectorId);
            setDataCheck("");
            setTempFile(tempFile);
        }

        public void setConnectorId(String connectorId)
        {
            this.connectorId = connectorId;
        }

        public void setDataCheck(String dataCheck)
        {
            this.dataCheck = dataCheck;
        }

        public void setTempFile(String tempFile)
        {
            this.tempFile = tempFile;
        }

        public String getCheckDeb()
        {

            if (dataCheck.Length == 0)
                return "";

            doc.LoadXml(dataCheck);

            String debId = doc.DocumentElement.FirstChild.Attributes.Item(0).Value;

            if (getAction(debId))
            {
                return dataCheck;
            }
            else
            {
                XmlNode node = doc.DocumentElement.FirstChild.FirstChild.Attributes.Item(0);
                node.Value = "insert";
                doc.DocumentElement.FirstChild.FirstChild.Attributes.SetNamedItem(node);
                
                XmlTextWriter xmlWriter = new XmlTextWriter(tempFile, null);
                xmlWriter.Formatting = Formatting.Indented;
                doc.Save(xmlWriter);
                xmlWriter.Close();

                StreamReader sr = new StreamReader(tempFile);

                string s = "";
                string dataXML = "";
                while ((s = sr.ReadLine()) != null)
                {
                    dataXML += s;
                }
                sr.Close();
                return dataXML;
            }
        }

        private bool getAction(string debId)
        {
            DataSet dataSet = BackgroundService.getConnector.GetDataSet(connectorId, getDebiteurFilter(debId));
            int result = dataSet.Tables["Connector_Debiteuren_Controle"].Rows.Count;

            if (result > 0)
                return true;
            else
                return false;
        }

        private string getDebiteurFilter(string debiteur)
        {
            StringWriter filterOutput = new StringWriter();
            XmlTextWriter filterXML = new XmlTextWriter(filterOutput);

            filterXML.Formatting = System.Xml.Formatting.Indented;
            filterXML.WriteStartElement("Filters");

            filterXML.WriteStartElement("Filter");
            filterXML.WriteStartAttribute("FilterId");
            filterXML.WriteValue("Filter1");
            filterXML.WriteEndAttribute();

            filterXML.WriteStartElement("Field");
            filterXML.WriteStartAttribute("FieldId");
            filterXML.WriteValue("Debiteur");
            filterXML.WriteStartAttribute("OperatorType");
            filterXML.WriteValue("1");
            filterXML.WriteEndAttribute();
            filterXML.WriteValue(debiteur);
            filterXML.WriteEndElement();

            filterXML.WriteEndElement();

            filterXML.WriteEndElement();

            return filterOutput.ToString();
        }

    }
}

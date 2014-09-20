using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Afas.ComConnectors
{
  public class GetConnector : ComConnector
  {
    private const int _outputModeXml = 1;
    private const int _schemaTypeDataSet = 2;
    private const bool _includeMetaData = true;

    public GetConnector(string environmentId, string userId, string password)
      : base("AfasGetConnector.Main")
    {
      InvokeMethod("Logon", environmentId, userId, password);
    }

    public string GetDataText(string connectorId, string filtersXml)
    {
      return InvokeMethod<string>("Execute", connectorId, OutputMode.Text, filtersXml, _includeMetaData, MarkupType.SemiColonRegionalSettings);
    }

    public string GetDataXml(string connectorId, string filtersXml)
    {
      return InvokeMethod<string>("Execute", connectorId, OutputMode.Xml, filtersXml, _includeMetaData, SchemaType.DataSet);
    }

    public DataSet GetDataSet(string connectorId, string filtersXml)
    {
      string dataXml = GetDataXml(connectorId, filtersXml);
      using(StringReader reader = new StringReader(dataXml))
      {
        DataSet data = new DataSet();
        data.ReadXml(reader);
        return data;
      }
    }
  }
}

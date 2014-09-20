using System;
using System.Collections.Generic;
using System.Text;

namespace Afas.ComConnectors
{
  public class UpdateConnector : ComConnector
  {
    public UpdateConnector(string environmentId, string userId, string password)
      : base("AfasConnector.Main") 
    {
      InvokeMethod("Logon", environmentId, userId, password);
    }

    private string ExecuteInternal(string dataXml)
    {
      string data = InvokeMethod<string>("Execute", dataXml);



      return data;
    }

    public string Execute(string connectorType, int connectorVersion, string dataXml)
    {
      InvokeMethod("Prepare", connectorType, connectorVersion);
      return ExecuteInternal(dataXml);
    }
  }
}

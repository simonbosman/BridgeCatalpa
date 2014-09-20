using System;
using System.Collections.Generic;
using System.Text;

namespace Afas.ComConnectors
{
  public enum SchemaType
  {
    Afas = 1,
    DataSet = 2
  }

  public enum OutputMode
  {
    Xml = 1,
    Text = 2
  }

  public enum MarkupType
  {
    SemiColonRegionalSettings = 1,
    TabRegionalSettings = 2,
    SemiColonAntaFormat = 3,
    TabAntaFormat = 4
  }
}

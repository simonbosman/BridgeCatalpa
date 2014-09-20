using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Afas.ComConnectors
{
  public abstract class ComConnector : IDisposable
  {
    #region =======================================================> FIELDS

    private object _comObject;
    private Type _comType;

    #endregion
    #region =======================================================> CONSTRUCTION

    public ComConnector(string progId)
    {
      _comType = Type.GetTypeFromProgID(progId);
      if(_comType != null)
      {
        _comObject = Activator.CreateInstance(_comType);
      }
      else
      {
        throw new Exception(string.Format("The Com type with progId: {0} could not be obtained.", progId));
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      if(disposing)
      {
        GC.SuppressFinalize(this);
      }

      if(_comObject != null)
      {
        Marshal.ReleaseComObject(_comObject);
      }
    }

    ~ComConnector()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
    }

    #endregion
    #region =======================================================> PROTECTED METHODS

    protected static T ConvertValue<T>(object value)
    {
      if(value is DBNull || value == null)
      {
        return default(T);
      }
      else
      {
        return (T)value;
      }
    }

    protected void InvokeMethod(string methodName, params object[] parameters)
    {
      InvokeMethodInternal(methodName, parameters);
    }

    protected TResult InvokeMethod<TResult>(string methodName, params object[] parameters)
    {
      return ConvertValue<TResult>(InvokeMethodInternal(methodName, parameters));
    }

    #endregion
    #region =======================================================> PRIVATE METHODS

    private object InvokeMethodInternal(string methodName, params object[] parameters)
    {
      return _comType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, _comObject, parameters);
    }

    #endregion
  }
}

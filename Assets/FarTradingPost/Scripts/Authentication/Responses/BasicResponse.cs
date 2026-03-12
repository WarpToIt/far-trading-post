using System ;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NUnit.Framework;
using Unity.VisualScripting;

namespace FarTrader.Authentication
{
  public class BasicResponse
  {
    private RawBasicResponse _response ;

#region Properties
    public bool OK => _response.errors.Length == 0 ;
    public List<string> Errors => _response.errors.ToList() ;
#endregion

    internal BasicResponse( RawBasicResponse response )
    {
      _response = response ;
    }

#region Serializables
    [Serializable]
    internal class RawBasicResponse
    {
      public string[] errors ;
    }
#endregion
  }
}
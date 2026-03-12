using System ;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NUnit.Framework;
using Unity.VisualScripting;

namespace FarTrader.Authentication
{
  public class SaltResponse
  {
    private RawSaltResponse _response ;

#region Properties
    public bool OK => _response.errors.Length == 0 ;
    public int Id => _response.id ;
    public string Salt => _response.salt ;
    public List<string> Errors => _response.errors.ToList() ;
#endregion

    internal SaltResponse( RawSaltResponse response )
    {
      _response = response ;
    }

#region Serializables
    [Serializable]
    internal class RawSaltResponse
    {
      public int id ;
      public string salt ;
      public string[] errors ;
    }
#endregion
  }
}
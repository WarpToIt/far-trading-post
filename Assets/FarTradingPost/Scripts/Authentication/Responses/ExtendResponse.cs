using System ;
using System.Collections.Generic;
using System.Linq;

namespace FarTrader.Authentication
{
  public class ExtendResponse
  {
    private RawExtendResponse _response ;

#region Properties
    public bool OK => _response.errors.Length == 0 ;
    public DateTime ExpiresAt => DateTime.Parse( _response.expires_at ) ;
    public List<string> Errors => _response.errors.ToList() ;
#endregion

    internal ExtendResponse( RawExtendResponse response )
    {
      _response = response ;
    }

#region Serializables
    [Serializable]
    internal class RawExtendResponse
    {
      public string expires_at ;
      public string[] errors ;
    }
#endregion
  }
}
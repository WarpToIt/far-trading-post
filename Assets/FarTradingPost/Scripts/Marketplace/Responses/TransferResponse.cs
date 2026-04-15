using System ;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NUnit.Framework;
using Unity.VisualScripting;

namespace FarTrader.Marketplace
{
  public class TransferResponse
  {
    private RawTransferResponse _response ;

#region Properties
    public bool OK => _response.errors.Length == 0 ;
    public int Uid => _response.uid ;
    public List<string> Errors => _response.errors.ToList() ;
#endregion

    internal TransferResponse( RawTransferResponse response )
    {
      _response = response ;
    }

#region Serializables
    [Serializable]
    internal class RawTransferResponse
    {
      public int uid ;
      public string[] errors ;
    }
#endregion
  }
}
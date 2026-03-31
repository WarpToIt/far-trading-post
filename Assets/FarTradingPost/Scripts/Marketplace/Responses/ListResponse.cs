using System ;
using System.Collections.Generic;
using System.Linq;

namespace FarTrader.Marketplace
{
  public class ListResponse
  {
    private readonly RawListResponse _response ;


    public List<InventoryRowData> Data => _response.data.ToList() ;
    public List<string> Errors => _response.errors.ToList() ;


#region Constructor
    internal ListResponse( RawListResponse response )
    {
      _response = response ;
    }
#endregion


#region Serializables
    [Serializable]
    public class RawListResponse
    {
    public InventoryRowData[] data ;
    public string[] errors ;
    }
#endregion
  }
}
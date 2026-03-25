using System ;
using System.Collections.Generic;
using System.Linq;
using FarTrader.Authentication;

namespace FarTrader.Marketplace
{
  public class ContextResponse
  {
#region Fields
    private readonly int _userId ;
    private readonly RawContextResponse _response ;
#endregion


// TODO: clean up raw sql data into friendly usable datatypes 
#region Properties
    public bool OK => _response.errors.Length == 0 ;
    public int UserId => _userId ;
    public List<ActorRowData> Actors => _response.actors.ToList() ;
    public List<CompanyRowData> Companies => _response.companies.ToList() ;
    public List<ItemPrototypesRowData> ItemPrototypes => _response.itemPrototypes.ToList() ;
    public List<CategoriesRowData> Categories => _response.categories.ToList() ;
    public List<TimestampsRowData> Timestamps => _response.timestamps.ToList() ;
    public List<ValueTrendsRowData> ValueTrends => _response.valueTrends.ToList() ;
    public List<string> Errors => _response.errors.ToList() ;
#endregion


#region Constructor
    internal ContextResponse( int userId, RawContextResponse response )
    {
      _userId     = userId ;
      _response = response ;
    }
#endregion


#region Serializables
    [Serializable]
    public class RawContextResponse
    {
      public ActorRowData[] actors ;
      public CompanyRowData[] companies ;
      public ItemPrototypesRowData[] itemPrototypes ;
      public CategoriesRowData[] categories ;
      public TimestampsRowData[] timestamps ;
      public ValueTrendsRowData[] valueTrends ;
      public string[] errors ;
    }
#endregion
  }
}
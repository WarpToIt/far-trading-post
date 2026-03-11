using System ;

namespace FarTrader.Marketplace
{
  [Serializable]
  public class ListResponse
  {
    public InventoryData[] data ;
    public string[] error ;
  }

  [Serializable]
  public class InventoryData
  {
    public int proto_id ;
    public int uid ;
    public int count ;
    public float want ;
  }
}
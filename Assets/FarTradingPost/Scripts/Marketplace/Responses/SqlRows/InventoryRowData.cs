using System ;

namespace FarTrader.Marketplace
{
  [Serializable]
  public class InventoryRowData
  {
    public int actor_id ;
    public int proto_id ;
    public int uid ;
    public int count ;
    public float want ;
  }
}
using System ;

namespace FarTrader.Marketplace
{
  [Serializable]
  public class ListResponse
  {
    public InventoryRowData[] data ;
    public string[] errors ;
  }
}
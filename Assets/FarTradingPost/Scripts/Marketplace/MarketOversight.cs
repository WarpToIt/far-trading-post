using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class MarketOversight : MonoBehaviour
  {
#region Unity Editor Fields
    [SerializeField] RectTransform itemWarehouse ;
    [SerializeField] GameObject marketItemPrefab ;
#endregion


#region Properties
    public readonly List<MarketItem> AllMarketItems = new () ;
    public readonly List<ItemPrototype> AllItemPrototypes = new () ;
#endregion


#region Item Management
    public void NewMarketItem( Actor owner, ItemPrototype itemPrototype )
    {
      MarketItem marketItem = Instantiate( marketItemPrefab ).GetComponent<MarketItem>() ;
      marketItem.InitializeFrom( owner, itemPrototype ) ;
      marketItem.GetComponent<RectTransform>().SetParent( itemWarehouse ) ;
      AllMarketItems.Add( marketItem ) ;
    }
#endregion


#region Retrieval Methods
    public IEnumerable<MarketItem> GetActorInventory( Actor actor ) => AllMarketItems.Where( (mItem) => mItem.Owner == actor ) ;
#endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FarTrader.Marketplace;
using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Navigation
{
  public class ItemListing : MonoBehaviour
  {
#region Fields
    private readonly List<ItemPlaque> content = new () ;
#endregion


#region Unity Editor Fields
    [SerializeField] private GameObject itemPlaquePrefab ;
    [SerializeField] private RectTransform viewportContent ;
#endregion


#region Properties
#endregion


#region Content Management
    public void ClearItems()
    {
      foreach( RectTransform child in viewportContent)
      {
        GameObject.Destroy( child.gameObject ) ;
      }
    }

    public void AddItems( IEnumerable<MarketItem> items )
    {
      foreach( MarketItem item in items)
      {
        AddItem( item ) ;
      }
    }

    public void AddItem(MarketItem item)
    {
      ItemPlaque itemPlaque = Instantiate( itemPlaquePrefab ).GetComponent<ItemPlaque>() ;
      itemPlaque.GetComponent<RectTransform>().SetParent( viewportContent ) ;
      itemPlaque.InitializeFrom( item ) ;
      itemPlaque.SetPlaqueSelected( plaqueSelected ) ;
      content.Add( itemPlaque ) ;
    }
#endregion


#region Events
    private readonly UnityEvent<ItemPlaque> plaqueSelected = new () ;
#endregion


#region Event Handlers
    private void OnPlaqueSelected( ItemPlaque plaque )
    {
      foreach( ItemPlaque itemPlaque in content )
      {
        if( itemPlaque != plaque )
        {
          itemPlaque.SetSelected( false ) ;
        }
      }
    }

    public void OnClickDiscard()
    {
      ItemPlaque item = content.First((p)=>p.IsSelected) ;
      // item.MarketItem is Null for some reason? 
      Debug.Log($"Make {item.MarketItem.Name} go kaboom!") ;
      MarketplaceEvents.MarketAction.Invoke( new MarketActionContext( MarketActions.Discard, item.MarketItem ) ) ;
    }

    public void OnClickBuy()
    {
      ItemPlaque item = content.First((p)=>p.IsSelected) ;
      // item.MarketItem is Null for some reason? 
      Debug.Log($"Buy {item.MarketItem.Name} from its owner!") ;
      MarketplaceEvents.MarketAction.Invoke( new MarketActionContext( MarketActions.Buy, item.MarketItem ) ) ;
    }

    public void OnClickSell()
    {
      ItemPlaque item = content.First((p)=>p.IsSelected) ;
      // item.MarketItem is Null for some reason? 
      Debug.Log($"Sell {item.MarketItem.Name} to a new owner!") ;
      MarketplaceEvents.MarketAction.Invoke( new MarketActionContext( MarketActions.Sell, item.MarketItem ) ) ;
    }
#endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      plaqueSelected.AddListener( OnPlaqueSelected ) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  }
}
using System.Collections.Generic;
using FarTrader.Marketplace;
using UnityEngine;

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
    public void AddItem(MarketItem item)
    {
      ItemPlaque itemPlaque = Instantiate( itemPlaquePrefab ).GetComponent<ItemPlaque>() ;
      itemPlaque.InitializeFrom( item ) ;
      itemPlaque.GetComponent<RectTransform>().SetParent( viewportContent ) ;
      content.Add( itemPlaque ) ;
    }
#endregion


#region Event Handlers
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
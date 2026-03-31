using System;
using FarTrader.Marketplace;
using FarTrader.Navigation;
using UnityEngine;

namespace FarTrader.Navigation
{
  [RequireComponent(typeof(NavigableScreen))]
  public class ItemListingWidget : MonoBehaviour
  {
    [SerializeField] private SelectorActor actorSelector ;
    [SerializeField] private ItemListing itemListing ;


#region Properties
    public Actor Actor => actorSelector.Selection ;
    public ItemListing ItemListing => itemListing ;
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
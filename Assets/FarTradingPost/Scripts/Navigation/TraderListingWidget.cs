using System;
using System.Collections.Generic;
using FarTrader.Marketplace;
using FarTrader.Navigation;
using UnityEngine;

namespace FarTrader.Navigation
{
  [RequireComponent(typeof(NavigableScreen))]
  public class TraderListingWidget : MonoBehaviour
  {
    [SerializeField] private SelectorRandomActors actorSelection ;
    [SerializeField] private TraderListing traderListing ;


#region Properties
    public SelectorRandomActors SelectionConfig => actorSelection ;
    public TraderListing TraderListing => traderListing ;
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
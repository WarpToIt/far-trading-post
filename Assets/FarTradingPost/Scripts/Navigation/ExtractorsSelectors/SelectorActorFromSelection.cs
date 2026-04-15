using System;
using FarTrader.Authentication;
using FarTrader.Marketplace;
using UnityEngine;



namespace FarTrader.Navigation
{
  public class SelectorActorFromSelection : SelectorActor
  {
    [SerializeField] TraderListing traderListing ;

    override public Actor Selection => traderListing.Selected.Actor ;

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
using System;
using FarTrader.Marketplace;
using UnityEngine;

namespace FarTrader.Navigation
{
  public class SelectorRandomActors : MonoBehaviour
  {
    [SerializeField] private RandomActorSelectionConfig config ;


    public bool Humans => config.humans ;
    public bool ActivePlayer => config.activePlayer ;
    public int Count => config.count ;

    public bool MatchCriteria( Actor actor )
    {
      bool result = actor.Id > 0 ;

      if( !actor.IsHuman )
        return result ;
      
      if( config.activePlayer && actor.IsActivePlayer )
        return result ;
        
      if( config.humans )
        return result ;

      return false ;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#region Serializables
    [Serializable]
    private class RandomActorSelectionConfig
    {
      public bool humans ;
      public bool activePlayer ;
      public int count ;
    }
#endregion
  }
}
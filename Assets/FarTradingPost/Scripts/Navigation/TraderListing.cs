using System;
using System.Collections.Generic;
using System.Linq;
using FarTrader.Marketplace;
using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Navigation
{
  public class TraderListing : MonoBehaviour
  {
#region Fields
    private readonly List<TraderPlaque> content = new () ;
#endregion


#region Unity Editor Fields
    [SerializeField] private GameObject traderPlaquePrefab ;
    [SerializeField] private RectTransform viewportContent ;
#endregion


#region Properties
    public TraderPlaque Selected => content.First((p)=>p.IsSelected) ;
#endregion


#region Content Management
    public void ClearActors()
    {
      foreach( RectTransform child in viewportContent)
      {
        GameObject.Destroy( child.gameObject ) ;
      }
    }

    public void AddActors( IEnumerable<Actor> actors )
    {
      foreach( Actor actor in actors)
      {
        AddActor( actor ) ;
      }
    }

    public void AddActor(Actor actor)
    {
      TraderPlaque traderPlaque = Instantiate( traderPlaquePrefab ).GetComponent<TraderPlaque>() ;
      traderPlaque.GetComponent<RectTransform>().SetParent( viewportContent ) ;
      traderPlaque.InitializeFrom( actor ) ;
      traderPlaque.SetPlaqueSelected( plaqueSelected ) ;
      content.Add( traderPlaque ) ;
    }
#endregion


#region Events
    private readonly UnityEvent<TraderPlaque> plaqueSelected = new () ;
#endregion


#region Event Handlers
    private void OnPlaqueSelected( TraderPlaque plaque )
    {
      foreach( TraderPlaque traderPlaque in content )
      {
        if( traderPlaque != plaque )
        {
          traderPlaque.SetSelected( false ) ;
        }
      }
    }

    public void OnClickTrade()
    {
      TraderPlaque traderPlaque = content.First((p)=>p.IsSelected) ;
      // item.TraderPlaque is Null for some reason? 
      Debug.Log($"Initiate trade with {traderPlaque.Actor.Name}!") ;
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
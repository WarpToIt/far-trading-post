using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using FarTrader.Navigation;
using NUnit.Framework.Constraints;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class MarketOversight : MonoBehaviour
  {
#region Unity Editor Fields
    [SerializeField] RectTransform companyRegistry ;
    [SerializeField] GameObject companyPrefab ;

    [SerializeField] RectTransform actorRepository ;
    [SerializeField] GameObject actorPrefab ;

    [SerializeField] RectTransform itemWarehouse ;
    [SerializeField] GameObject marketItemPrefab ;
#endregion


#region Properties
    private readonly List<Actor> _actors = new () ;
    private readonly List<Company> _companies = new () ;
    private readonly List<MarketItem> _marketItems = new () ;
    private readonly List<ItemPrototype> _itemPrototypes = new () ;
    private readonly List<ItemCategory> _categories = new () ;
    private readonly List<GameTimestamp> _timestamps = new () ;
    private readonly List<ItemValueTrend> _valueTrends = new () ;
#endregion


#region Item Management
    public void NewCompany( CompanyRowData data )
    {
      Company company = Instantiate( companyPrefab ).GetComponent<Company>() ;
      company.InitializeFrom( data ) ;
      company.GetComponent<RectTransform>().SetParent( companyRegistry ) ;
      _companies.Add( company ) ;
    }

    public void NewActor( ActorRowData data )
    {
      Actor actor = Instantiate( actorPrefab ).GetComponent<Actor>() ;
      actor.InitializeFrom( data.id, data.name, GetCompanyById(data.id) ) ;
      actor.GetComponent<RectTransform>().SetParent( actorRepository ) ;
      _actors.Add( actor ) ;
    }

    public void NewMarketItem( Actor owner, ItemPrototype itemPrototype, int itemUid, int count, float want )
    {
      MarketItem marketItem = Instantiate( marketItemPrefab ).GetComponent<MarketItem>() ;
      marketItem.InitializeFrom( owner, itemPrototype, itemUid, count, want ) ;
      marketItem.GetComponent<RectTransform>().SetParent( itemWarehouse ) ;
      _marketItems.Add( marketItem ) ;
    }
#endregion


#region Retrieval Methods
    public IEnumerable<MarketItem> GetActorInventory( Actor actor ) => _marketItems.Where( (mItem) => mItem.Owner == actor ) ;
    public Actor GetActorById( int id ) => _actors.FirstOrDefault( (e) => e.Id == id ) ;
    public Actor GetActorOfPlayer() => _actors.FirstOrDefault( (e) => e.IsActivePlayer ) ;
    public Company GetCompanyById( int id ) => _companies.FirstOrDefault( (e) => e.Id == id ) ;
    public ItemCategory GetCagtegoryById( int id ) => _categories.FirstOrDefault( (e) => e.Id == id ) ;
    public ItemPrototype GetPrototypeById( int id ) => _itemPrototypes.FirstOrDefault( (e) => e.Id == id ) ;
    public GameTimestamp GetTimestampById( int id ) => _timestamps.FirstOrDefault( (e) => e.Id == id ) ;
    public bool TryGetTimestampById( int id, out GameTimestamp timestamp )
    {
      timestamp = _timestamps.FirstOrDefault( (e) => e.Id == id ) ;
      return timestamp != default ;
    }
#endregion


#region Event Handlers
    public void OnUpdateItemListing( ItemListingWidget itemListingWidget )
    {
      itemListingWidget.ItemListing.AddItems( GetActorInventory( GetActorById( itemListingWidget.Actor.Id ) ) ) ;
    }
#endregion


#region Initialization
    public void OnContextReceived( ContextResponse context )
    {
      Debug.Log( context ) ;

      foreach( CompanyRowData row in context.Companies )
      {
        NewCompany( row ) ;
      }
#if UNITY_EDITOR
      Debug.Log($"{_companies.Count} Companies loaded from context.");
#endif

      foreach( ActorRowData row in context.Actors )
      {
        NewActor( row ) ;
      }
#if UNITY_EDITOR
      Debug.Log($"{_actors.Count} Actors loaded from context.");
#endif

      foreach( CategoriesRowData row in context.Categories )
      {
        _categories.Add( new ItemCategory( row ) ) ;
      }
#if UNITY_EDITOR
      Debug.Log($"{_categories.Count} Categories loaded from context.");
#endif

      foreach( ItemPrototypesRowData row in context.ItemPrototypes )
      {
        _itemPrototypes.Add( new ItemPrototype( row, GetCagtegoryById( row.category ) ) ) ;
      }
#if UNITY_EDITOR
      Debug.Log($"{_itemPrototypes.Count} ItemPrototypes loaded from context.");
#endif

      foreach( TimestampsRowData row in context.Timestamps )
      {
        _timestamps.Add( new GameTimestamp( row ) ) ;
      }
#if UNITY_EDITOR
      Debug.Log($"{_timestamps.Count} Timestamps loaded from context.");
#endif

      foreach( ValueTrendsRowData row in context.ValueTrends )
      {
        ItemCategory category     = GetCagtegoryById( row.category_id ) ;
        if( TryGetTimestampById( row.timestamp_id, out GameTimestamp timestamp) )
        {
          ItemValueTrend valueTrend = new( category, timestamp, row.trend ) ;
          _valueTrends.Add( valueTrend ) ;
          category.ValueTrends.Add( valueTrend ) ;
        }
        else
        {
          Debug.Log( $"Failed to find Timestamp with Id {row.timestamp_id}" ) ;
        }
      }
#if UNITY_EDITOR
      Debug.Log($"{_valueTrends.Count} Value Trends loaded from context.");
#endif

      MarketplaceEvents.ContextLoaded.Invoke( GetActorById( context.UserId ) ) ;
    }
    
    public void OnInventoryReceived( ListResponse response )
    {
      foreach( InventoryRowData row in response.Data )
      {
        NewMarketItem( GetActorById( row.actor_id ), GetPrototypeById( row.proto_id ), row.uid, row.count, row.want ) ;
      }
#if UNITY_EDITOR
      Debug.Log($"{_marketItems.Count} MarketItems loaded from response.");
#endif
    }
#endregion


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
    public class ItemListingByActor
    {
      public int id ;
      public ItemListing itemListing ;
    }

    [Serializable]
    public class ItemListingByItems
    {
      public IEnumerable<MarketItem> items ;
      public ItemListing itemListing ;
    }
#endregion
  }
}
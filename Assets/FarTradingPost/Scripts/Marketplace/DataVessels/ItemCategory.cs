using System;
using System.Collections.Generic;
using UnityEngine;


namespace FarTrader.Marketplace
{
  public class ItemCategory
  {
#region Fields
    private readonly List<ItemValueTrend> _valueTrends = new () ;
#endregion


#region Properties
    public int Id { get ; protected set ; }
    public string Name { get ; protected set ; }
    public List<ItemValueTrend> ValueTrends => _valueTrends ;
#endregion


#region Constructor
    internal ItemCategory( CategoriesRowData data ) : this( data.id, data.name ) { }

    internal ItemCategory( int id, string name )
    {
      Id = id ;
      Name = name ;
    }
#endregion
  }
}
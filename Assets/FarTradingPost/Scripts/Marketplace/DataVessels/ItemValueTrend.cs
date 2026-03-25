using System;
using UnityEngine;


namespace FarTrader.Marketplace
{
  public class ItemValueTrend
  {
#region Fields

#endregion


#region Properties
    public ItemCategory Category { get ; protected set ; }
    public GameTimestamp Timestamp { get ; protected set ; }
    public float Trend { get ; protected set ; }
#endregion


#region Constructor
    internal ItemValueTrend( ItemCategory category, GameTimestamp timestamp, float trend )
    {
      Category  = category ;
      Timestamp = timestamp ;
      Trend     = trend ;
    }
#endregion
  }
}
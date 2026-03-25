using System;
using UnityEngine;


namespace FarTrader.Marketplace
{
  public class GameTimestamp
  {
#region Fields

#endregion


#region Properties
    public int Id { get ; protected set ; }
    public DateTime Timestamp { get ; protected set ; }
#endregion


#region Constructor
    internal GameTimestamp( TimestampsRowData data ) : this( data.id, DateTime.Parse( data.timestamp ) ) { }

    internal GameTimestamp( int id, DateTime timestamp )
    {
      Id = id ;
      Timestamp = timestamp ;
    }
#endregion
  }
}
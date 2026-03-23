using System;
using UnityEngine;


namespace FarTrader.Marketplace
{
  public class ItemPrototype
  {
#region Fields
      private readonly string name ;
      private readonly string category ;
      private readonly int unitValue ;
#endregion


#region Properties
    public string Name => name ;
    public string Category => category ;
    public int UnitValue => unitValue ;
#endregion


#region Constructor
    public ItemPrototype( ItemPrototypeData data ) : this( data.name, data.category, data.unitValue ) { }

    public ItemPrototype( string name, string category, int unitValue )
    {
      this.name       = name ;
      this.category   = category ;
      this.unitValue  = unitValue ;
    }
#endregion


#region Serializables
    [Serializable]
    public class ItemPrototypeData
    {
#region Fields
      public string name ;
      public string category ;
      public int unitValue ;
#endregion
    }
#endregion
  }
}
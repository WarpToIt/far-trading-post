using System;
using UnityEngine;


namespace FarTrader.Marketplace
{
  public class ItemPrototype
  {
#region Fields
      private readonly int id ;
      private readonly string name ;
      private readonly ItemCategory category ;
      private readonly int unitValue ;
#endregion


#region Properties
    public int Id => id ;
    public string Name => name ;
    public ItemCategory Category => category ;
    public int UnitValue => unitValue ;
#endregion


#region Constructor
    public ItemPrototype( ItemPrototypesRowData data, ItemCategory category ) : this( data.id, data.name, category, data.value ) { }
    //public ItemPrototype( ItemPrototypeData data ) : this( data.name, data.category, data.unitValue ) { }

    public ItemPrototype( int id, string name, ItemCategory category, int value )
    {
      this.id         = id ;
      this.name       = name ;
      this.category   = category ;
      this.unitValue  = value ;
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
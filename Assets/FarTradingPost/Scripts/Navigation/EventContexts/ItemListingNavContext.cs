using System;
using static FarTrader.Marketplace.MarketOversight;

namespace FarTrader.Navigation
{
  public class ItemListingNavContext
  {
#region Properties
    public NavigableScreen NavigableScreen { get; private set ; }
    public ItemListingByActor ItemListingByActor { get; private set ; }
#endregion


    public ItemListingNavContext( NavigableScreen navigableScreen, ItemListingByActor isUnlocked )
    {
      NavigableScreen = navigableScreen ;
      ItemListingByActor = isUnlocked ;
    }
  } 
}
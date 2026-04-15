
using FarTrader.Authentication;

namespace FarTrader.Marketplace
{
  public class MarketActionContext
  {
    public Actor Actor { get ; private set ; }
    public MarketItem Item { get ; private set ; }
    public MarketActions Verb { get ; private set ; }
    public int Count { get ; private set ; }
    
    internal MarketActionContext( MarketActions verb, MarketItem item ) : this( null, verb, item, 1 )
    { }
    
    internal MarketActionContext( Actor actor, MarketActions verb, MarketItem item ) : this( actor, verb, item, 1 )
    { }

    internal MarketActionContext( Actor actor, MarketActions verb, MarketItem item, int count )
    {
      Actor = actor ;
      Verb  = verb ;
      Item  = item ;
      Count = count ;
    }
  }
}

using FarTrader.Authentication;
using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Marketplace
{
  public class MarketplaceEvents : MonoBehaviour
  {
    private static MarketplaceEvents _instance ;

#region Unity Editor
    [SerializeField] private UnityEvent<ContextResponse> contextReceived ;
    [SerializeField] private UnityEvent<ListResponse> inventoryReceived ;
    [SerializeField] private UnityEvent<Actor> contextLoaded ;
#endregion


#region Static Properties
    public static UnityEvent<ContextResponse> ContextReceived => _instance.contextReceived ;
    public static UnityEvent<ListResponse> InventoryReceived => _instance.inventoryReceived ;
    public static UnityEvent<Actor> ContextLoaded => _instance.contextLoaded ;
#endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      if( _instance == null )
      {
        _instance = this ;
      } else {
        Destroy(gameObject) ;
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  }
}

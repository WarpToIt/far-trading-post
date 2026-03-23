using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Marketplace
{
  public class MarketplaceEvents : MonoBehaviour
  {
    private static MarketplaceEvents _instance ;

#region Unity Editor
    [SerializeField] private UnityEvent contextReceived  ;
#endregion


#region Static Properties
    public static UnityEvent ContextReceived => _instance.contextReceived ;
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

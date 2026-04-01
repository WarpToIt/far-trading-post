using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Navigation
{
  public class NavigationEvents : MonoBehaviour
  {
    private static NavigationEvents _instance ;

#region Unity Editor
    [SerializeField] private UnityEvent<OkDialogContext> triggerOkDialog ;
    [SerializeField] private UnityEvent<ItemListingWidget> updateItemListing ;
    [SerializeField] private UnityEvent<TraderListingWidget> updateTraderListing ;
    [SerializeField] private UnityEvent unlockOkDialog ;
    [SerializeField] private UnityEvent openOverview ;
    [SerializeField] private UnityEvent returnToLogin ;
#endregion


#region Static Properties
    public static UnityEvent<OkDialogContext> TriggerOkDialog => _instance.triggerOkDialog ;
    public static UnityEvent<ItemListingWidget> UpdateItemListing => _instance.updateItemListing ;
    public static UnityEvent<TraderListingWidget> UpdateTraderListing => _instance.updateTraderListing ;
    public static UnityEvent UnlockOkDialog => _instance.unlockOkDialog ;
    public static UnityEvent TriggerOpenOverview => _instance.openOverview ;
    public static UnityEvent TriggerReturnToLogin => _instance.returnToLogin ;
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

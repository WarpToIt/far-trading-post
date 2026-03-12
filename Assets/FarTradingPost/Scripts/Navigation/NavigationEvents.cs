using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Navigation
{
  public class NavigationEvents : MonoBehaviour
  {
    private static NavigationEvents _instance ;

#region Unity Editor
    [SerializeField] private UnityEvent<OkDialogContext> okDialog ;
    [SerializeField] private UnityEvent openOverview ;
#endregion


#region Static Properties
    public static UnityEvent<OkDialogContext> TriggerOkDialog => _instance.okDialog ;
    public static UnityEvent TriggerOpenOverview => _instance.openOverview ;
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

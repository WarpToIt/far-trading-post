using UnityEngine;
using UnityEngine.Events;

namespace FarTrader.Authentication
{
  public class AuthenticationEvents : MonoBehaviour
  {
    private static AuthenticationEvents _instance ;

#region Unity Editor
    [SerializeField] private UnityEvent userAuthenticated ;
#endregion


#region Static Properties
    public static UnityEvent UserAuthenticated => _instance.userAuthenticated ;
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

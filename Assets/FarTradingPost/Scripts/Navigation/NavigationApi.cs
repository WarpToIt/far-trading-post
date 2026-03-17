using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FarTrader.Navigation
{
  public class NavigationApi : MonoBehaviour
  {
#region Unity Editor
    [SerializeField] private NavigableScreen initialScreen ;
    [SerializeField] private NavScreens navigableScreens ;
#endregion


#region Properites
    public NavigableScreen Login => navigableScreens.loginScreen ;
    public NavigableScreen Registration => navigableScreens.registrationScreen ;
    public NavigableScreen Overview => navigableScreens.overviewScreen ;
    public NavigableScreen Inventory => navigableScreens.inventoryScreen ;
    public List<NavigableScreen> All => new() ;
#endregion


#region Event Handlers
    public void OnTriggerReturnToLogin()
    {
      All.ForEach( (screen) => { screen.Close() ; } ) ;
      Login.Open() ;
    }
#endregion


#region API Actions
    public void QuitApplication()
    {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#else
      Application.Quit() ;
#endif
    }
#endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      All.Add( Login ) ;
      All.Add( Registration ) ;
      All.Add( Overview ) ;
      All.Add( Inventory ) ;

      if( initialScreen != null )
      {
        initialScreen.Show() ;
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    private class NavScreens
    {
      public NavigableScreen loginScreen ;
      public NavigableScreen registrationScreen ;
      public NavigableScreen overviewScreen ;
      public NavigableScreen inventoryScreen ;
    }
  }
}
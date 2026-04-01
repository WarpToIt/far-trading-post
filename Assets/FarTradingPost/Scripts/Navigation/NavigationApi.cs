using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FarTrader.Navigation
{
  public class NavigationApi : MonoBehaviour
  {
#region Fields
      private readonly List<NavigableScreen> _all = new () ;
#endregion


#region Unity Editor
    [SerializeField] private NavigableScreen initialScreen ;
    [SerializeField] private NavScreens navigableScreens ;
#endregion


#region Properites
    public NavigableScreen Login => navigableScreens.loginScreen ;
    public NavigableScreen Registration => navigableScreens.registrationScreen ;
    public NavigableScreen Overview => navigableScreens.overviewScreen ;
    public NavigableScreen Inventory => navigableScreens.inventoryScreen ;
    public NavigableScreen Trade => navigableScreens.tradeScreen ;
#endregion


#region Event Handlers
    public void OnTriggerReturnToLogin()
    {
      _all.ForEach( (screen) => { screen.Close() ; } ) ;
      Login.Open() ;
    }

    public void OnClickNavigateTo(NavigableScreen navigableScreen)
    {
      _all.ForEach( (screen) => { screen.Close() ; } ) ;
      if( navigableScreen.gameObject.TryGetComponent<ItemListingWidget>( out ItemListingWidget itemListingWidget ) )
      {
        NavigationEvents.UpdateItemListing.Invoke( itemListingWidget ) ;
      }
      if( navigableScreen.gameObject.TryGetComponent<TraderListingWidget>( out TraderListingWidget traderListingWidget ) )
      {
        NavigationEvents.UpdateTraderListing.Invoke( traderListingWidget ) ;
      }
      navigableScreen.Open() ;
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
      _all.Add( Login ) ;
      _all.Add( Registration ) ;
      _all.Add( Overview ) ;
      _all.Add( Inventory ) ;
      _all.Add( Trade ) ;

      if( initialScreen != null )
      {
        initialScreen.Show() ;
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


#region Serializables
    [Serializable]
    private class NavScreens
    {
      public NavigableScreen loginScreen ;
      public NavigableScreen registrationScreen ;
      public NavigableScreen overviewScreen ;
      public NavigableScreen inventoryScreen ;
      public NavigableScreen tradeScreen ;
    }
#endregion
  }
}
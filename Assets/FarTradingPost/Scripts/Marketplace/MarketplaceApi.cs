using System;
using System.Collections;
using System.Text;
using FarTrader.Authentication;
using FarTrader.Navigation;
using FarTrader.Request;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class MarketplaceApi : MonoBehaviour
  {
#region Unity Editor Fields
    [SerializeField] ServerInfo server ;
    [SerializeField] InventoryEndpoints endpoints ;
    [SerializeField] MarketOversight marketOversight ;
#endregion


#region Fields
    private User _user ;
#endregion


#region Unity Actions
    public void OnUserAuthenticated(User user)
    {
      _user = user ;
      StartCoroutine( Context( _user.Id, _user.Token.Key, (ctx) =>
      {
        MarketplaceEvents.ContextReceived.Invoke( ctx ) ;
      } ) ) ;
    }

    public void OnContextLoaded( Actor actor )
    {
      _user.Actor = actor ;
      StartCoroutine( List( _user.Id, _user.Token.Key, (response) =>
      {
        MarketplaceEvents.InventoryReceived.Invoke( response ) ;
      } ) ) ;
      NavigationEvents.UnlockOkDialog.Invoke() ;
    }
#endregion


#region API Actions
    public IEnumerator Context(int id, string token, Action<ContextResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.GET,
          $"http://{server.Host}:{server.Port}{endpoints.context}",
          new string[] { id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ }}" ),
          (s) => { onResult?.Invoke( new ContextResponse( id, JsonUtility.FromJson<ContextResponse.RawContextResponse>(s) ) ) ; }
        )
      ) ;
    }

    public IEnumerator List(int id, string token, Action<ListResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.GET,
          $"http://{server.Host}:{server.Port}{endpoints.list}",
          new string[] { id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ }}" ),
          (s) => { onResult?.Invoke( new ListResponse( JsonUtility.FromJson<ListResponse.RawListResponse>(s) ) ) ; }
        )
      ) ;
    }

    public IEnumerator Give(int id, string token, int uid, int count, int want, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"POST {endpoints.give}" ) ;
    }

    public IEnumerator Edit(int id, string token, int uid, int count, int want, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"PUT {endpoints.edit}" ) ;
    }

    public IEnumerator Transfer(int id, int target_id, string token, int uid, int count, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"PUT {endpoints.transfer}" ) ;
    }

    public IEnumerator Remove(int id, string token, int uid, int count, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"DELETE {endpoints.remove}" ) ;
    }
#endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


#region Serializables
    [Serializable]
    private class InventoryEndpoints
    {
      public string context = "/inventory/context/{id}/{token}" ;
      public string list = "/inventory/{id}/{token}" ;
      public string give = "/inventory/{id}/{token}" ;
      public string edit = "/inventory/{id}/{token}" ;
      public string transfer = "/inventory/{id}/{target_id}/{token}" ;
      public string remove = "/inventory/{id}/{token}" ;
    }
#endregion
  }
}
using System;
using System.Collections;
using System.Linq;
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


#region Market Actions
    public void OnMarketAction( MarketActionContext ctx )
    {
      switch( ctx.Verb )
      {
        case MarketActions.Invalid:
        case MarketActions.None:
          Debug.Log($"Something went wrong!") ;
          break ;
        case MarketActions.Discard:
          OnMarketActionDiscard( ctx ) ;
          break ;
        case MarketActions.Sell:
          OnMarketActionSell( ctx ) ;
          break ;
        case MarketActions.Buy:
          OnMarketActionBuy( ctx ) ;
          break ;
        default:
          Debug.Log($"Something went wrong!") ;
          break ;
      }
    }

    private void OnMarketActionDiscard( MarketActionContext ctx )
    {
      StartCoroutine( Remove( _user.Id, _user.Token.Key, ctx.Item.Uid, ctx.Count,
      (response) => {
          if( !response.OK )
            throw new Exception( $"Errors: {String.Join( ", ", response.Errors)}" ) ;
        } )
      ) ; // remove item(s)

      ctx.Item.Remove( ctx.Count ) ;

      // safeguard item count >= 0 ?
    }
    
    private void OnMarketActionSell( MarketActionContext ctx )
    {
      // TODO: check request validity
      // - owner of item is NOT the buyer
      // - buyer has sufficient currency in their inventory

      // TODO: transfer currency to the seller

      StartCoroutine( Transfer( _user.Id, _user.Token.Key, ctx.Actor.Id, ctx.Item.Uid, ctx.Count,
      (response) => {
          if( !response.OK )
            throw new Exception( $"Errors: {String.Join( ", ", response.Errors)}" ) ;
        } )
      ) ; // transfer item(s) to new owner

      // make local changes to currency and item ownership
    }

    private void OnMarketActionBuy( MarketActionContext ctx )
    {
      // TODO: check request validity
      // - owner of item is NOT the buyer
      // - buyer has sufficient currency in their inventory

      // TODO: transfer currency to the seller

      StartCoroutine( Transfer( _user.Id, _user.Token.Key, _user.Id, ctx.Item.Uid, ctx.Count,
      (response) => {
          if( !response.OK )
            throw new Exception( $"Errors: {String.Join( ", ", response.Errors)}" ) ;
        } )
      ) ; // transfer item(s) to new owner

      // make local changes to currency and item ownership
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

    public IEnumerator Give(int id, string token, int uid, int count, int want, Action<BasicResponse> onResult)
    {
      yield return null ;
     
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"POST {endpoints.give}" ) ;
    }

    public IEnumerator Edit(int id, string token, int uid, int count, int want, Action<BasicResponse> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"PUT {endpoints.edit}" ) ;
    }

    public IEnumerator Transfer(int id, string token, int target_id, int uid, int count, Action<TransferResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.PUT,
          $"http://{server.Host}:{server.Port}{endpoints.transfer}",
          new string[] { id.ToString(), uid.ToString(), count.ToString(), target_id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ }}" ),
          (s) => { onResult?.Invoke( new TransferResponse( JsonUtility.FromJson<TransferResponse.RawTransferResponse>(s) ) ) ; }
        )
      ) ;
    }

    public IEnumerator Remove(int id, string token, int uid, int count, Action<BasicResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.DELETE,
          $"http://{server.Host}:{server.Port}{endpoints.remove}",
          new string[] { id.ToString(), uid.ToString(), count.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ }}" ),
          (s) => { onResult?.Invoke( new BasicResponse( JsonUtility.FromJson<BasicResponse.RawBasicResponse>(s) ) ) ; }
        )
      ) ;
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
      public string transfer = "/inventory/{id}/{uid}/{count}/{target_id}/{token}" ;
      public string remove = "/inventory/{id}/{uid}/{count}/{token}" ;
    }
#endregion
  }
}
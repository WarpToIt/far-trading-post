using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace FarTrader.Request
{
  public class RequestDispatcher : MonoBehaviour
  {
    private static RequestDispatcher _instance ;

    private static Regex _paramPattern = new (@"{[a-zA-Z]+}") ;

    public static IEnumerator Dispatch( RequestType requestType, string uri, string[] uriParams, byte[] data, Action<string> onResult ) =>
      _instance.SendHttpRequest( requestType, uri, uriParams, data, onResult ) ;

    private IEnumerator SendHttpRequest( RequestType requestType, string uri, string[] uriParams, byte[] data, Action<string> onResult )
    {
      /** Construct URI */
      
      if( _paramPattern.Matches(uri).Count != uriParams.Length )
        throw new ArgumentException( $"A {requestType}-request to URI \"{uri}\" takes {_paramPattern.Matches(uri).Count} parameters, but {uriParams.Length} were given.") ;

      for( int i = 0; i < uriParams.Length; i++)
      {
        uri = _paramPattern.Replace(uri,uriParams[i]) ;
      }
      /** End */


      /** Make Web Request */
      using UnityWebRequest webRequest = new ( uri, requestType.ToString() );

      if (data != null)
        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
      webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
      webRequest.SetRequestHeader("Content-Type", "application/json");

#if UNITY_EDITOR
      Debug.Log($"Dispatching a {requestType}-request to \"{uri}\".");
#endif
      yield return webRequest.SendWebRequest();

      if (webRequest.result != UnityWebRequest.Result.Success)
      {
#if UNITY_EDITOR
        Debug.Log($"Web-request encountered an error: {webRequest.error}");
#endif
        yield break;
      }
      else
      {
#if UNITY_EDITOR
        Debug.Log($"Received: {webRequest.downloadHandler.text}");
#endif
      }
      /** End */


      /** Handle Response Body */
      onResult?.Invoke(webRequest.downloadHandler.text);
      /** End */
    }

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
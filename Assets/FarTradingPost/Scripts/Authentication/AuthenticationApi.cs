using System;
using System.Collections;
using System.Data.Common;
using UnityEngine;
using FarTrader.Request ;
using System.Text;
using TMPro;
using UnityEngine.InputSystem.Composites;
using FarTrader.Navigation;

namespace FarTrader.Authentication 
{
  public class AuthenticationApi : MonoBehaviour
  {
#region Unity Editor
    [SerializeField] ServerInfo server ;
    [SerializeField] AuthEndpoints endpoints ;
#endregion


#region Unity Actions
    public void OnClickLogin( LoginExtractor loginData )
    {
      StartCoroutine(
        Salt( loginData.EmailRaw,
          (response) =>
          {
            if( response.OK )
              StartCoroutine(
                Login( response.Id, loginData.PasswordRaw + response.Salt, (response) =>
                  {
                    if( response.OK )
                    {
                      Debug.Log( $"Received token ({response.Token}) for user {response.UserName}. Expires: {response.ExpiresAt}") ;
                      AuthenticationEvents.UserAuthenticated.Invoke() ;
                      NavigationEvents.TriggerOkDialog.Invoke( new OkDialogContext( $"Welcome back, {response.UserName}!", () => { NavigationEvents.TriggerOpenOverview.Invoke() ; } ) ) ;
                    }
                    else
                    {
                      Debug.Log( $"Failed to complete login because the process encountered the following errors: {response.Errors}" ) ;
                    }
                  }
                )
              ) ;
            else
              Debug.Log( $"Failed to retrieve salt because the process encountered the following errors: {response.Errors}" ) ;
          }
        )
      ) ;
    }

    public void OnClickRegister( RegistrationExtractor registrationData )
    {
      if( !registrationData.PwdMatch )
        throw new ArgumentException( "Submitted passwords don't match." ) ;
      
      StartCoroutine(
        Register(
          registrationData.UsernameRaw,
          registrationData.EmailRaw,
          registrationData.PasswordRaw,
          (response) =>
          {
            if( response.OK )
              Debug.Log( $"Registration successful.") ;
            else
              Debug.Log( $"Encountered the following errors: {response.Errors}" ) ;
          }
        )
      ) ;
    }
#endregion


#region API Actions

    /// <summary>
    /// Attempt to procure the salt of the user associated with the provided details.
    /// </summary>
    /// <param name="onResult">The SaltResponse will contain the user's id and the corresponding salt.</param>
    public IEnumerator Salt(string email, Action<SaltResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.GET,
          $"http://{server.Host}:{server.Port}{endpoints.salt}",
          new string[] {} ,
          Encoding.UTF8.GetBytes( $"{{ \"email\": \"{email}\" }}" ),
          (s) => { onResult?.Invoke( new SaltResponse( JsonUtility.FromJson<SaltResponse.RawSaltResponse>(s) ) ) ; }
        )
      ) ;
    }

    /// <summary>
    /// Attempt to use the prodided details to request a session token from the authentication database.
    /// </summary>
    /// <param name="password">The raw password will be hashed before dispatching.</param>
    /// <param name="onResult">The LoginResponse will contain the user's id, username, token, and token expiry information.</param>
    public IEnumerator Login(int id, string password, Action<LoginResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.GET,
          $"http://{server.Host}:{server.Port}{endpoints.login}",
          new string[] { id.ToString() } ,
          Encoding.UTF8.GetBytes( $"{{ \"passkey\": \"{password.GetHashCode()}\" }}" ),
          (s) => { onResult?.Invoke( new LoginResponse( JsonUtility.FromJson<LoginResponse.RawLoginResponse>(s) ) ) ; }
        )
      ) ;
    }

    /// <summary>
    /// Attempt to use the prodided details to request an extension on the lifetime of the current session token from the authentication database.
    /// </summary>
    /// <param name="onResult">The ExtendResponse will contain the new expiry time.</param>
    public IEnumerator Extend(int id, string token, Action<ExtendResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.PUT,
          $"http://{server.Host}:{server.Port}{endpoints.extend}",
          new string[] { id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ }}" ),
          (s) => { onResult?.Invoke( new ExtendResponse( JsonUtility.FromJson<ExtendResponse.RawExtendResponse>(s) ) ) ; }
        )
      ) ;
    }

    /// <summary>
    /// Attempt to request the immediat/premature expiry of the current session token. 
    /// </summary>
    /// <param name="onResult">The BasicResponse will be OK if the request was successful.</param>
    public IEnumerator Logout(int id, string token, Action<BasicResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.DELETE,
          $"http://{server.Host}:{server.Port}{endpoints.logout}",
          new string[] { id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ }}" ),
          (s) => { onResult?.Invoke( new BasicResponse( JsonUtility.FromJson<BasicResponse.RawBasicResponse>(s) ) ) ; }
        )
      ) ;
    }

    /// <summary>
    /// Attempt to register a new user in the authentication database with the prodided details.
    /// </summary>
    /// <param name="password">The raw password will be hashed before dispatching.</param>
    /// <param name="onResult">The BasicResponse will be OK if the request was successful.</param>
    public IEnumerator Register(string username, string email, string password, Action<BasicResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.POST,
          $"http://{server.Host}:{server.Port}{endpoints.register}",
          new string[] {} ,
          Encoding.UTF8.GetBytes( $"{{ \"username\": \"{username}\", \"email\": \"{email}\", \"passkey\": \"{password.GetHashCode()}\" }}" ),
          (s) => { onResult?.Invoke( new BasicResponse( JsonUtility.FromJson<BasicResponse.RawBasicResponse>(s) ) ) ; }
        )
      ) ;
    }

    /// <summary>
    /// Attempt to un-register an existing user in the authentication database with the prodided details.
    /// </summary>
    /// <param name="password">The raw password will be hashed before dispatching.</param>
    /// <param name="onResult">The BasicResponse will be OK if the request was successful.</param>
    public IEnumerator UnRegister(int id, string token, string password, Action<BasicResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.DELETE,
          $"http://{server.Host}:{server.Port}{endpoints.unregister}",
          new string[] { id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ \"passkey\": \"{password.GetHashCode()}\" }}" ),
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
    private class AuthEndpoints
    {
      public string salt = "/salt" ;
      public string login = "/auth/{id}" ;
      public string extend = "/auth/{id}/{token}" ;
      public string logout = "/auth/{id}/{token}" ;
      public string register = "/user" ;
      public string unregister = "/user/{id}/{token}" ;
    }
#endregion
  }
}
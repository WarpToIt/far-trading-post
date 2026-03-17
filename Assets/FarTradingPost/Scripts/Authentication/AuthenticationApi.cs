using System;
using System.Collections;
using System.Data.Common;
using UnityEngine;
using FarTrader.Request ;
using System.Text;
using TMPro;
using UnityEngine.InputSystem.Composites;
using FarTrader.Navigation;
using System.Security.Cryptography;

namespace FarTrader.Authentication 
{
  public class AuthenticationApi : MonoBehaviour
  {
#region Unity Editor
    [SerializeField] User user ;
    [SerializeField] ServerInfo server ;
    [SerializeField] AuthEndpoints endpoints ;
#endregion


#region Unity Actions
    public void OnClickLogin( LoginExtractor loginData ) =>
      LoginUser( loginData.EmailRaw, loginData.PasswordRaw, () =>
      {
        NavigationEvents.TriggerOkDialog.Invoke( new OkDialogContext( $"Welcome back, {user.Name}!", () => { NavigationEvents.TriggerOpenOverview.Invoke() ; } ) ) ;
      } ) ;

    public void OnClickLogout() =>
      LogoutUser( () => { AuthenticationEvents.UserLoggedOut.Invoke( user ) ; } ) ;

    public void OnClickRegister( RegistrationExtractor registrationData )
    {
      if( !registrationData.PwdMatch )
      {
        NavigationEvents.TriggerOkDialog.Invoke( new OkDialogContext( $"Submitted passwords don't match.", () => {} ) ) ;  
        //throw new ArgumentException( "Submitted passwords don't match." ) ;
      }

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
              Debug.Log( $"Encountered the following errors: {string.Join( ",",response.Errors)}" ) ;
          }
        )
      ) ;
    }
#endregion


#region User Actions
    public void LoginUser( string email, string password, Action onResult = null )
    {
      if( user.IsLoggedIn )
        throw new Exception( $"User {user.Name} is already logged in!" ) ;

      StartCoroutine(
        Salt( email,
          (resSalt) =>
          {
            if( resSalt.OK )
              StartCoroutine(
                Login( resSalt.Id, HashPasskey( password, resSalt.Salt ), (resLogin) =>
                  {
                    if( resLogin.OK )
                    {
                      Debug.Log( $"Received token ({resLogin.Token}) for user {resLogin.UserName}. Expires: {resLogin.ExpiresAt}") ;

                      user.Id    = resSalt.Id ;
                      user.Name  = resLogin.UserName ;
                      user.Email = email ;
                      user.Token = new SessionToken( resLogin.Token, resLogin.ExpiresAt ) ;
                      
                      AuthenticationEvents.UserAuthenticated.Invoke( user ) ;
                      onResult?.Invoke() ;
                      StartCoroutine( MaintainSession() ) ;
                    }
                    else
                    {
                      Debug.Log( $"Failed to complete login because the process encountered the following errors: {string.Join( ",",resLogin.Errors)}" ) ;
                    }
                  }
                )
              ) ;
            else
              Debug.Log( $"Failed to retrieve salt because the process encountered the following errors: {string.Join( ",",resSalt.Errors)}" ) ;
          }
        )
      ) ;
    }

    public void ExtendUserSession( Action onResult = null)
    {
      if( user.Id < 0 || user.Token == null )
        return ;

      StartCoroutine( Extend( user.Id, user.Token.Key, (response) => { user.Token.ExpiresAt = response.ExpiresAt ; onResult?.Invoke() ; } ) ) ;
    }

    public void LogoutUser( Action onResult = null )
    {
      if( user.Id < 0 || user.Token == null )
        return ;

      StartCoroutine( Logout( user.Id, user.Token.Key, (r) => {
        user.Clear() ;
        onResult?.Invoke() ;
      } ) ) ;
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
    /// <param name="passkey">The hashed passkey.</param>
    /// <param name="onResult">The LoginResponse will contain the user's id, username, token, and token expiry information.</param>
    public IEnumerator Login(int id, string passkey, Action<LoginResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.GET,
          $"http://{server.Host}:{server.Port}{endpoints.login}",
          new string[] { id.ToString() } ,
          Encoding.UTF8.GetBytes( $"{{ \"passkey\": \"{passkey}\" }}" ),
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
    /// <param name="passkey">The hashed passkey.</param>
    /// <param name="onResult">The BasicResponse will be OK if the request was successful.</param>
    public IEnumerator Register(string username, string email, string passkey, Action<BasicResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.POST,
          $"http://{server.Host}:{server.Port}{endpoints.register}",
          new string[] {} ,
          Encoding.UTF8.GetBytes( $"{{ \"username\": \"{username}\", \"email\": \"{email}\", \"passkey\": \"{passkey}\" }}" ),
          (s) => { onResult?.Invoke( new BasicResponse( JsonUtility.FromJson<BasicResponse.RawBasicResponse>(s) ) ) ; }
        )
      ) ;
    }

    /// <summary>
    /// Attempt to un-register an existing user in the authentication database with the prodided details.
    /// </summary>
    /// <param name="passkey">The hashed passkey.</param>
    /// <param name="onResult">The BasicResponse will be OK if the request was successful.</param>
    public IEnumerator UnRegister(int id, string token, string passkey, Action<BasicResponse> onResult)
    {
      yield return StartCoroutine(
        RequestDispatcher.Dispatch(
          RequestType.DELETE,
          $"http://{server.Host}:{server.Port}{endpoints.unregister}",
          new string[] { id.ToString(), token } ,
          Encoding.UTF8.GetBytes( $"{{ \"passkey\": \"{passkey}\" }}" ),
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


#region Session Maintenance
    private IEnumerator MaintainSession( float initialSleep = 600.0f, float periodicSleep = 60.0f, int extensionThreshold = 90 )
    {
      yield return new WaitForSecondsRealtime( initialSleep ) ;

      while( user.IsLoggedIn )
      {
        if( (user.Token.ExpiresAt - DateTime.Now) < new TimeSpan(0,0,extensionThreshold) )
        {
          ExtendUserSession() ;
        }

        yield return new WaitForSecondsRealtime( periodicSleep ) ;
      }
    }
#endregion


#region Cryptography
    private string HashPasskey( string passkey, string salt ) =>
      BitConverter.ToString(
        new SHA256CryptoServiceProvider()
          .ComputeHash( Encoding.UTF8.GetBytes( passkey + salt ) )
      ) ;
#endregion


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
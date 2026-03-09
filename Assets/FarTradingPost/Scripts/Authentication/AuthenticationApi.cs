using System;
using System.Collections;
using System.Data.Common;
using UnityEngine;

namespace FarTrader.Authentication 
{
  public class AuthenticationApi : MonoBehaviour
  {
    [SerializeField] ServerInfo server ;
    [SerializeField] AuthEndpoints endpoints ;

    public IEnumerator Login(string email, string password, Action<LoginResponse> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"GET {endpoints.login}" ) ;
    }

    public IEnumerator Extend(int id, string token, Action<ExtendResponse> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"PUT {endpoints.extend}" ) ;
    }

    public IEnumerator Logout(int id, string token, Action<LogoutResponse> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"DELETE {endpoints.logout}" ) ;
    }

    public IEnumerator Register(string username, string email, string password, string confirmPassword, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"POST {endpoints.register}" ) ;
    }

    public IEnumerator UnRegister(int id, string token, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"DELETE {endpoints.unregister}" ) ;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    [Serializable]
    private class AuthEndpoints
    {
      public string login = "/auth" ;
      public string extend = "/auth/{id}" ;
      public string logout = "/auth/{id}" ;
      public string register = "/user" ;
      public string unregister = "/user/{id}" ;
    }
  }
}
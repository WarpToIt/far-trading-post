using System;
using System.Collections;
using UnityEngine;

namespace FarTrader.Authentication 
{
  public class AuthenticationApi : MonoBehaviour
  {
    [SerializeField] ServerInfo server ;
    [SerializeField] AuthEndpoints endpoints ;

    public IEnumerator Login(string email, string passkey, Action<LoginResponse> onResult)
    {
      Debug.Log( endpoints.login ) ; // GET
      yield return null ;
      onResult?.Invoke(default) ;
    }

    public IEnumerator Extend(string token, Action<ExtendResponse> onResult)
    {
      Debug.Log( endpoints.extend ) ; // PUT
      yield return null ;
      onResult?.Invoke(default) ;
    }

    public IEnumerator Logout(string token, Action<LogoutResponse> onResult)
    {
      Debug.Log( endpoints.logout ) ; // DELETE
      yield return null ;
      onResult?.Invoke(default) ;
    }

    public IEnumerator Register(string email, string passkey)
    {
      Debug.Log( endpoints.register ) ; // POST
      yield return null ;
    }

    public IEnumerator UnRegister(string email, string passkey)
    {
      Debug.Log( endpoints.unregister ) ; // DELETE
      yield return null ;
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
      public string unregister = "/user" ;
    }
  }
}
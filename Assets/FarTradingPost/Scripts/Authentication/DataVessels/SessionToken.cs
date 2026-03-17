using System;

namespace FarTrader.Authentication
{
  public class SessionToken
  {
#region Properties
    public string Key { get ; internal set ; }
    public DateTime ExpiresAt { get ; internal set ; } 
#endregion

    internal SessionToken( string key, DateTime expiry)
    {
      Key = key ;
      ExpiresAt = expiry ; 
    }
  }
}
using System;
using UnityEngine;

namespace FarTrader.Authentication
{
  public class User : MonoBehaviour
  {
#region Properties
    public int Id { get ; internal set ; } = -1 ;
    public string Name { get ; internal set ; } = string.Empty ;
    public string Email { get ; internal set ; } = string.Empty ;
    public SessionToken Token { get ; internal set ; } = null ;
#endregion


#region Derived Properties
    public bool IsLoggedIn => Token != null && Token.ExpiresAt > DateTime.Now ;
#endregion


#region Methods
    internal void Clear()
    {
      Id    = -1 ;
      Name  = string.Empty ;
      Email = string.Empty ;
      Token = null ;
    }
#endregion
  }
}
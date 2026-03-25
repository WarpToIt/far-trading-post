using System;
using FarTrader.Marketplace;
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

    public Actor Actor { get ; internal set ; } = null ;
#endregion


#region Derived Properties
    public bool IsLoggedIn => HasValidToken && HasAssignedActor ;
    public bool HasValidToken => Token != null && Token.ExpiresAt > DateTime.Now ;
    public bool HasAssignedActor => Actor != null ;
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
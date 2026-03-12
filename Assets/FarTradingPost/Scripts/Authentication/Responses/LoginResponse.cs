using System ;

namespace FarTrader.Authentication
{
  [Serializable]
  public class LoginResponse
  {
    public int id ;
    public string username ;
    public string token ;
    public DateTime expires_at ;
    public string error ;
  }
}
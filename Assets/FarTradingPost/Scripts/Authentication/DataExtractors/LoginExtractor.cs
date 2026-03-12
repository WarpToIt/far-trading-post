using TMPro;
using UnityEngine;

namespace FarTrader.Authentication 
{
  public class LoginExtractor : MonoBehaviour
  {
    [SerializeField] private TMP_InputField email ;
    [SerializeField] private TMP_InputField password ;

    public string EmailRaw => email.text ;
    public string PasswordRaw => password.text ;
  }
}
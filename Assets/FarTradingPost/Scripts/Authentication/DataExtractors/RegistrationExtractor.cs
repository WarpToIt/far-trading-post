using TMPro;
using UnityEngine;

namespace FarTrader.Authentication 
{
  public class RegistrationExtractor : MonoBehaviour
  {
    [SerializeField] private TMP_InputField username ;
    [SerializeField] private TMP_InputField email ;
    [SerializeField] private TMP_InputField password ;
    [SerializeField] private TMP_InputField repeatPassword ;

    public string UsernameRaw => username.text ;
    public string EmailRaw => email.text ;
    public string PasswordRaw => password.text ;
    public string RepeatPasswordRaw => repeatPassword.text ;

    public bool PwdMatch => password.text == repeatPassword.text ;
  }
}
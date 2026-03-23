using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarTrader.Navigation
{
  public class DialogWindow : NavigableScreen
  {
#region Fields
    private Action onDismiss ;
#endregion


#region Unity Editor
    [SerializeField] private TextMeshProUGUI message ;
    [SerializeField] private Button button ;
#endregion


#region Event Handlers
    public void OnTriggerOkDialog( OkDialogContext ctx )
    {
      message.text = ctx.Message ;
      button.enabled = ctx.IsUnlocked ;
      onDismiss = ctx.OnDismiss ;
      Show() ;
    }

    public void OnUnlockOkDialog()
    {
      button.enabled = true ;
    }

    public void OnDismiss()
    {
      onDismiss?.Invoke() ;
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
  }
}
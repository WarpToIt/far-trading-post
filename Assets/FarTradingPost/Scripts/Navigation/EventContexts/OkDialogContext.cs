using System;

namespace FarTrader.Navigation
{
  public class OkDialogContext
  {
#region Properties
    public string Message { get; private set ; }
    public Action OnDismiss { get ; private set ; }
#endregion


    public OkDialogContext( string message, Action onDismiss )
    {
      Message = message ;
      OnDismiss = onDismiss ;
    }
  } 
}
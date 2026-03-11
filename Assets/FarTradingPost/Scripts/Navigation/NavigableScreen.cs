using UnityEngine;

namespace FarTrader.Navigation
{
public class NavigableScreen : MonoBehaviour
{
  private Vector3 _offsetPosition = new ( -2000.0f, 0.0f, 0.0f ) ;

#region Properties
  public bool IsVisible { get ; private set ; } = false ;
#endregion

  public void Open() {}
  public void Close() {}
  public void Show()
  {
    if( IsVisible )
      return ;
    
    transform.position -= _offsetPosition ;
    IsVisible = true ;
  }
  public void Hide()
  {
    if( !IsVisible )
      return ;
    
    transform.position += _offsetPosition ;
    IsVisible = false ;
  }

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
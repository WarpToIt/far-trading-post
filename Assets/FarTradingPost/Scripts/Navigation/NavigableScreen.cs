using UnityEngine;

namespace FarTrader.Navigation
{
  public class NavigableScreen : MonoBehaviour
  {
#region Fields
    private Vector3 _offsetPosition = new ( -2000.0f, 0.0f, 0.0f ) ;
#endregion


#region Unity Editor Fields
    [SerializeField] private bool hasSidePanel ;
    [SerializeField] private NavigableScreen sidePanel ;
#endregion


#region Properties
    public bool IsVisible { get ; private set ; } = false ;
#endregion


#region API Actions
    public void Open()
    {
      // apply/update values
      Show() ;
    }

    public void Close()
    {
      // reset/clear values
      Hide() ;
    }

    public void Show()
    {
      if( IsVisible )
        return ;
      
      if( hasSidePanel )
        sidePanel.Show() ;

      transform.position -= _offsetPosition ;
      IsVisible = true ;
    }
    public void Hide()
    {
      if( !IsVisible )
        return ;
      
      if( hasSidePanel )
        sidePanel.Hide() ;
      
      transform.position += _offsetPosition ;
      IsVisible = false ;
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
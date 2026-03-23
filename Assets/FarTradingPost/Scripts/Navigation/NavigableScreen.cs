using UnityEngine;

namespace FarTrader.Navigation
{
  public class NavigableScreen : MonoBehaviour
  {
#region Fields
    private Vector3 _basePosition ;
    private Vector3 _offsetPosition ;
    private Vector3 _positionOffset = new ( -2000.0f, 0.0f, 0.0f ) ;
#endregion


#region Unity Editor Fields
    [SerializeField] private bool hasSidePanel ;
    [SerializeField] private NavigableScreen sidePanel ;
    [SerializeField] private bool isVisible = false ;
#endregion


#region Properties
    public bool IsVisible { get { return isVisible ; } private set { isVisible = value ; } }
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

      IsVisible = true ;
      
      if( hasSidePanel ) { sidePanel.Show() ; }

      transform.localPosition = _basePosition ;
    }

    public void Hide()
    {
      if( !IsVisible )
        return ;

      IsVisible = false ;
      
      if( hasSidePanel ) { sidePanel.Hide() ; }
      
      transform.localPosition = _offsetPosition ;
    }
#endregion


    // Awake is called when the script instance is being loaded
    void Awake()
    {
      _offsetPosition = transform.localPosition ;
      _basePosition = _offsetPosition - _positionOffset ;
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
using FarTrader.Authentication;
using FarTrader.Marketplace;
using UnityEngine;

namespace FarTrader.Navigation
{
  public class NavigationTargetWidget : MonoBehaviour
  {
    [SerializeField] private NavigableScreen navigationTarget ;
    [SerializeField] private User user ;
    [SerializeField] private MarketOversight marketOversight ;
    [SerializeField] private GameObject parameter2 ;
    [SerializeField] private GameObject parameter3 ;


#region Properties
    public NavigableScreen Target => navigationTarget ;
    public User User => user ;
    public MarketOversight Market => marketOversight ;
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
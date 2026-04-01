using System;
using FarTrader.Marketplace;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FarTrader.Navigation
{
  public class TraderPlaque : MonoBehaviour
  {
#region Fields
    private UnityEvent<TraderPlaque> plaqueSelected ;
#endregion


#region Unity Editor Fields
    [SerializeField] private TextMeshProUGUI labelName ;
    [SerializeField] private TextMeshProUGUI labelCompany ;
    [SerializeField] private RawImage traderIcon ;

    [SerializeField] private TraderPlaqueTrendInfo trend1 ;
    [SerializeField] private TraderPlaqueTrendInfo trend2 ;

    [SerializeField] private TraderPlaqueSelectionInfo selectionInfo ;
#endregion


#region Properties
    public bool IsSelected { get ; private set ; } = false ;
    public Actor Actor { get ; private set ; }
#endregion


#region Event Handlers
    public void OnClick() => ToggleSelected() ;
#endregion


#region Selection
    public void ToggleSelected()
    {
      IsSelected = !IsSelected ;
      ApplySelectionToPanel() ;
    }
    
    public void SetSelected(bool value)
    {
      IsSelected = value ;
      ApplySelectionToPanel() ;
    }
    
    public void MarkSelected()
    {
      IsSelected = true ;
      ApplySelectionToPanel() ;
    }
    
    public void ClearSelected()
    {
      IsSelected = false ;
      ApplySelectionToPanel() ;
    }

    private void ApplySelectionToPanel()
    {
      selectionInfo.backgroundPanel.color = IsSelected ? selectionInfo.selected : selectionInfo.unselected ;
      if( IsSelected )
      {
        plaqueSelected.Invoke(this) ;
      }
    }
#endregion


#region Value Setters
    public void SetPlaqueSelected( UnityEvent<TraderPlaque> value ) => plaqueSelected = value ;
#endregion


#region Initialization
    public void InitializeFrom(Actor actor)
    {
      this.Actor        = actor ;
      labelName.text    = this.Actor.Name ;
      labelCompany.text = this.Actor.Company.Name ;
      // itemIcon.texture  = this.Actor.Icon ;
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


#region Serializables
    [Serializable]
    private class TraderPlaqueTrendInfo
    {
      public TextMeshProUGUI trendName ;
      public RawImage trendIndicator ;
    }

    [Serializable]
    private class TraderPlaqueSelectionInfo
    {
      public Color unselected = Color.white ;
      public Color selected   = Color.aliceBlue ;
      public Image backgroundPanel ;
    }
#endregion
  }
}

using System;
using FarTrader.Marketplace;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FarTrader.Navigation
{
  public class ItemPlaque : MonoBehaviour
  {
#region Fields
    private UnityEvent<ItemPlaque> plaqueSelected ;
#endregion


#region Unity Editor Fields
    [SerializeField] private TextMeshProUGUI labelName ;
    [SerializeField] private TextMeshProUGUI labelCategory ;
    [SerializeField] private RawImage trendIndicator ;
    [SerializeField] private ItemPlaqueLabeledValues laeledValues ;
    [SerializeField] private RawImage itemIcon ;
    [SerializeField] private ItemPlaqueSelectionInfo selectionInfo ;
#endregion


#region Properties
    public bool IsSelected { get ; private set ; } = false ;
    public MarketItem MarketItem { get ; private set ; }
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
    public void SetUnitValue(int value)   => laeledValues.unitValue.SetNumberValue( value ) ;
    public void SetSumValue(int value)    => laeledValues.sumValue.SetNumberValue( value ) ;
    public void SetOwnedAvailLabel(string value)  => laeledValues.availOwned.SetLabelValue( value ) ;
    public void SetOwnedAvailValue(int value)     => laeledValues.availOwned.SetNumberValue( value ) ;
    public void SetPlaqueSelected( UnityEvent<ItemPlaque> value ) => plaqueSelected = value ;
#endregion


#region Initialization
    public void InitializeFrom(MarketItem marketItem)
    {
      this.MarketItem     = marketItem ;
      labelName.text      = this.MarketItem.Name ;
      labelCategory.text  = this.MarketItem.Category.Name ; // TODO: extract trade good name
      SetUnitValue( this.MarketItem.UnitValue ) ;
      SetOwnedAvailLabel( this.MarketItem.Owner.IsActivePlayer ? "Owned" : "Avail" ) ;
      SetOwnedAvailValue( this.MarketItem.Count ) ;
      SetSumValue( this.MarketItem.SumValue ) ;
      //trendIndicator.texture = [trendInfo] ;
      itemIcon.texture = this.MarketItem.Icon ;
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
    private class ItemPlaqueLabeledValues
    {
      public LabelValueDIsplay unitValue ;
      public LabelValueDIsplay sumValue ;
      public LabelValueDIsplay availOwned ;
    }

    [Serializable]
    private class ItemPlaqueSelectionInfo
    {
      public Color unselected = Color.white ;
      public Color selected   = Color.aliceBlue ;
      public Image backgroundPanel ;
    }
#endregion
  }
}

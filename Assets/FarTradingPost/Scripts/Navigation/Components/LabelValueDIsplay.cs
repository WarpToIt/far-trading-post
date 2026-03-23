using TMPro;
using UnityEngine;


namespace FarTrader.Navigation
{
  public class LabelValueDIsplay : MonoBehaviour
  {
#region Unity Editor Fields
    [SerializeField] private TextMeshProUGUI label ;
    [SerializeField] private TextMeshProUGUI value ;
#endregion


#region Value Setters    
    public void SetLabelValue(string value) => this.label.text = value ;
    public void SetNumberValue(int value) => this.value.text = value.ToString() ;
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
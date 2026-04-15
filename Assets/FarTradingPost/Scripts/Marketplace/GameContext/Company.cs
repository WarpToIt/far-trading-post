using System.Collections.Generic;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class Company : MonoBehaviour
  {
#region Unity Editor Fields

#endregion


#region Properites
    public int Id { get ; protected set ; }
    public string Name { get ; protected set ; }
#endregion


#region Initialization
    internal void InitializeFrom( CompanyRowData data )
    {
      Id   = data.id ;
      Name = data.name ;
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

using System.Collections.Generic;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class Actor : MonoBehaviour
  {
#region Unity Editor Fields

#endregion


#region Properites
    public int Id { get ; protected set ; }
    public string Name { get ; protected set ; }
    public Company Company { get ; protected set ; }
    public bool IsHuman { get ; protected set ; }
    public bool IsActivePlayer { get ; protected set ; }
#endregion


#region Initialization
    internal void InitializeFrom( int id, string name, Company company, bool human )
    {
      Id      = id ;
      Name    = name ;
      Company = company ;
      IsHuman = human ;
    }
#endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      IsActivePlayer = false ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  }
}

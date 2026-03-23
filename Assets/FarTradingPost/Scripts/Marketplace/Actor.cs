using System.Collections.Generic;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class Actor : MonoBehaviour
  {
#region Unity Editor Fields

#endregion


#region Properites
    public bool IsActivePlayer { get ; protected set ; }
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

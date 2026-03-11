using System;
using System.Collections;
using UnityEngine;

namespace FarTrader.Marketplace
{
  public class MarketplaceApi : MonoBehaviour
  {
#region Unity Editor
    [SerializeField] ServerInfo server ;
    [SerializeField] InventoryEndpoints inventoryEndpoints ;
#endregion


#region API Actions
    public IEnumerator Login(int id, string token, Action<ListResponse> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"GET {inventoryEndpoints.list}" ) ;
    }

    public IEnumerator Give(int id, string token, int uid, int count, int want, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"GET {inventoryEndpoints.list}" ) ;
    }

    public IEnumerator Edit(int id, string token, int uid, int count, int want, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"GET {inventoryEndpoints.edit}" ) ;
    }

    public IEnumerator Transfer(int id, int target_id, string token, int uid, int count, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"GET {inventoryEndpoints.transfer}" ) ;
    }

    public IEnumerator Remove(int id, string token, int uid, int count, Action<object> onResult)
    {
      yield return null ;
      onResult?.Invoke(default) ;
      throw new NotImplementedException( $"GET {inventoryEndpoints.remove}" ) ;
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
    private class InventoryEndpoints
    {
      public string list = "/inventory/{id}" ;
      public string give = "/inventory/{id}" ;
      public string edit = "/inventory/{id}" ;
      public string transfer = "/inventory/{id}/{target_id}" ;
      public string remove = "/inventory/{id}" ;
    }
#endregion
  }
}
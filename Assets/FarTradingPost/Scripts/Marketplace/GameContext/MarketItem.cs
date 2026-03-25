using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


namespace FarTrader.Marketplace
{
  public class MarketItem : MonoBehaviour
  {
#region Fields
    private ItemPrototype itemPrototype ;
#endregion


#region Unity Editor Fields
    [SerializeField] private Actor owner ;
    [SerializeField] private string itemName ;
    [SerializeField] private TradeGoodCategory itemCategory ;
    [SerializeField] private int unitValue ;
    [SerializeField] private int count ;
    [SerializeField] private float want ;
    [SerializeField] private Texture2D icon ;
#endregion


#region Properties
    public Actor Owner => owner ;
    public string Name => itemName ;
    public TradeGoodCategory Category => itemCategory ;
    public int UnitValue => unitValue ;
    public int Count => count ;
    public float Want => want ;
    public int SumValue => UnitValue * Count ;
    public Texture2D Icon => icon ;
#endregion


#region Actions
    public void TransferOwnership( Actor target )
    {
      owner = target ;
    }
#endregion


#region Initialization
    public void InitializeFrom( Actor owner, ItemPrototype itemPrototype )
    {
      this.owner = owner ;
      this.itemPrototype = itemPrototype ;
      // TODO: value assignment
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
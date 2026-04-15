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
    [SerializeField] private ItemCategory itemCategory ;
    [SerializeField] private int unitValue ;
    [SerializeField] private int uid ;
    [SerializeField] private int count ;
    [SerializeField] private float want ;
    [SerializeField] private Texture2D icon ;
#endregion


#region Properties
    public Actor Owner => owner ;
    public string Name => itemPrototype.Name ;
    public ItemCategory Category => itemPrototype.Category ;
    public int Uid => uid ;
    public int UnitValue => itemPrototype.UnitValue ;
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
    public void InitializeFrom( Actor owner, ItemPrototype itemPrototype, int itemUid, int count, float want )
    {
      this.owner  = owner ;
      this.itemPrototype = itemPrototype ;
      this.uid    = itemUid ;
      this.count  = count ;
      this.want   = want ;
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
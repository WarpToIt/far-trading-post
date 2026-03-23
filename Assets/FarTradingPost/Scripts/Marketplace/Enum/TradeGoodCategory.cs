namespace FarTrader.Marketplace
{
  public enum TradeGoodCategory
  {
    Invalid               = 0x000,
    None                  = 0x001,

    CommonElectronics       = 0x011,
    CommonIndustrialGoods   = 0x012,
    CommonManufacturedGoods = 0x013,
    CommonRawMaterials      = 0x014,
    CommonConsumables       = 0x015,
    CommonOre               = 0x016,

    AdvancedElectronics       = 0x021,
    AdvancedMachineParts      = 0x022,
    AdvancedManufacturedGoods = 0x023,
    AdvancedWeapons           = 0x024,
    AdvancedVehicles          = 0x025,
    Biochemicals              = 0x026,
    
    // TODO: finish list entries
  }
}
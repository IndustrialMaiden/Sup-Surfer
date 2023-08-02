using System;

namespace CodeBase.Components.Shop
{
    [Serializable]
    public class MainPurchase<TPurchaseType>
    {
        public string Id;
        public int Price;
        public TPurchaseType Value;
    }
    
    [Serializable]
    public class BoolPurchase : MainPurchase<bool>
    {
        
    }
    
    [Serializable]
    public class IntPurchase : MainPurchase<int>
    {
        
    }
}
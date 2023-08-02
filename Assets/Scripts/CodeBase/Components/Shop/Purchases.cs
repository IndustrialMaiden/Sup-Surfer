using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Components.Shop
{
    [CreateAssetMenu(fileName = "Purchases", menuName = "Shop/Purchases", order = 0)]
    public class Purchases : ScriptableObject
    {
        [SerializeField] public List<BoolPurchase> BoolPurchases;
        [SerializeField] public List<IntPurchase> IntPurchases;
    }
}
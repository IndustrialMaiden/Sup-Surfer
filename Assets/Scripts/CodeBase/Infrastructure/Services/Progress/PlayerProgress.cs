using System;
using System.Collections.Generic;
using CodeBase.Components.Shop;

namespace CodeBase.Infrastructure.Services.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Score;
        public int RewardCount;
        public int Credits;
        public DateTime LastClaimedTime;

        public List<BoolPurchase> BoolPurchases;
        public List<IntPurchase> IntPurchases;
    }
}
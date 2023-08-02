using System;

namespace CodeBase.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        void ShowRewardedAds(Action<bool> onRewardedClose, Action onRewardedStart = null);
        void ShowFullscreenAds();
    }
}
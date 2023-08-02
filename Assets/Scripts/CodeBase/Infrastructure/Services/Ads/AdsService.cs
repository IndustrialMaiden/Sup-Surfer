using System;
using CodeBase.Infrastructure.Services.Audio;
using GamePush;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsService : IAdsService
    {
        private readonly IAudioService _audioService;

        public AdsService(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        public void ShowRewardedAds(Action<bool> onRewardedClose, Action onRewardedStart = null)
        {
            onRewardedStart += () =>
            {
                _audioService.PauseMusic();
                _audioService.MuteAll();
                GP_Game.Pause();
            };
            onRewardedClose += delegate(bool b)
            {
                GP_Game.Resume();
                _audioService.UnmuteAll();
                _audioService.ResumeMusic();
            };
            GP_Ads.ShowRewarded(onRewardedStart: onRewardedStart, onRewardedClose: onRewardedClose);
        }

        public void ShowFullscreenAds()
        {
            void OnFullscreenStart()
            {
                _audioService.PauseMusic();
                _audioService.MuteAll();
                GP_Game.Pause();
            }

            void OnFullscreenClose(bool b)
            {
                GP_Game.Resume();
                _audioService.UnmuteAll();
                _audioService.ResumeMusic();
            }

            GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
        }
    }
}
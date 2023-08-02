using System;
using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Utils.Disposables;
using GamePush;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components.DailyReward
{
    public class DailyRewardComponent : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Text _text;

        [SerializeField] private int _creditsCount;

        private int _rewardCount;

        [SerializeField] private float _claimCooldown;

        private bool _canClaimReward;

        private CompositeDisposables _trash = new CompositeDisposables();

        private Animator _animator;

        private DateTime _lastClaimedTime;

        private PlayerProgress _progress;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _progress = AllServices.Container.Single<IPlayerProgressService>().Progress;
            LoadProgress(_progress);
        }

        private void Start()
        {
#if UNITY_EDITOR
            _claimCooldown = _claimCooldown / 10 / 3 ;
#endif
            _trash.Retain(_rewardButton.onClick.Subscribe(OnRewardButtonClicked));
            StartCoroutine(UpdateRewardTime());
        }

        private void OnRewardButtonClicked()
        {
            AllServices.Container.Single<IAdsService>().ShowRewardedAds(onRewardedClose: delegate(bool b) { OnRewardedAdSuccess(); });
            

#if UNITY_EDITOR
            AllServices.Container.Single<IAudioService>().PlayOneShotSfx("Buy");
            ClaimReward();
#endif
        }
        
        private void OnRewardedAdSuccess()
        {
            AllServices.Container.Single<IAudioService>().PlayOneShotSfx("Buy");
            ClaimReward();
            UpdateRewardUI();
        }

        private IEnumerator UpdateRewardTime()
        {
            while (true)
            {
                UpdateRewardState();
                yield return new WaitForSecondsRealtime(1f);
            }
        }

        private void UpdateRewardState()
        {
            var timeSpan = GP_Server.Time() -
                           AllServices.Container.Single<IPlayerProgressService>().Progress.LastClaimedTime;
            
            _canClaimReward = !(timeSpan.TotalMinutes < _claimCooldown);
            
            UpdateRewardUI();
        }

        private void UpdateRewardUI()
        {

            _animator.enabled = _canClaimReward;

            _rewardCount = _creditsCount * AllServices.Container.Single<IPlayerProgressService>().Progress.RewardCount;
            
            _rewardButton.interactable = _canClaimReward;

            if (_canClaimReward) {
                
                _text.text = _rewardCount.ToString();
            }
            else
            {
                _text.text = "00:00";
                
                var nextClaimTime = AllServices.Container.Single<IPlayerProgressService>().Progress.LastClaimedTime.AddMinutes(_claimCooldown);
                var currentClaimCooldown = nextClaimTime - GP_Server.Time();

                string cd =
                    $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                _text.text = cd;
            }
        }

        public void ClaimReward()
        {
            if (!_canClaimReward) return;

            Game.Player.Credits.Value += _rewardCount;
            
            UpdateProgress(_progress);
            
            AllServices.Container.Single<ISaveLoadService>().SaveProgress();

            _canClaimReward = false;
            
            UpdateRewardState();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _lastClaimedTime = progress.LastClaimedTime;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LastClaimedTime = GP_Server.Time();
            progress.RewardCount += 1;
        }
    }
}
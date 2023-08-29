using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.SceneChanger;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class FinishScreen : WindowBase
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;

        private ISceneChangerService _sceneChanger;
        private IAdsService _adsService;

        protected override void OnAwake()
        {
            _sceneChanger = AllServices.Container.Single<ISceneChangerService>();
            _adsService = AllServices.Container.Single<IAdsService>();
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        protected override void Start()
        {
            Game.Pause();
        }

        private void OnRestartButtonClick()
        {
            _adsService.ShowFullscreenAds();
            _sceneChanger.Enter("Main");
            Destroy(gameObject);
        }

        private void OnResumeButtonClick()
        {
            Game.Resume();
            Game.Player.IsAlive = true;
            Destroy(gameObject);
            //_adsService.ShowRewardedAds(onRewardedClose: OnRewardedClose);
        }

        private void OnRewardedClose(bool obj)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnDestroy()
        {
            _resumeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}
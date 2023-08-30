using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SceneChanger;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class PauseScreen : WindowBase
    {
        private const string MainScene = "Main";
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;

        private ISceneChangerService _sceneChanger;
        
        protected override void OnAwake()
        {
            _sceneChanger = AllServices.Container.Single<ISceneChangerService>();
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }
        
        protected override void Start()
        {
            Game.Pause();
        }

        private void OnRestartButtonClick()
        {
            _sceneChanger.LoadScene(MainScene);
            Destroy(gameObject);
        }

        private void OnResumeButtonClick()
        {
            AllServices.Container.Single<IUIFactory>().CreateInstructionsScreen();
            Game.Player.IsAlive = true;
            Destroy(gameObject);
        }
        
        protected override void OnDestroy()
        {
            _resumeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}
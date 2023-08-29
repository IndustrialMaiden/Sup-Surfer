using CodeBase.Components;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Localization;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.SceneChanger;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using GamePush;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<ILocalizationService>(new LocalizationService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssets>(), 
                _services.Single<IStaticDataService>(),
                _services.Single<IPlayerProgressService>()));
            
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(), 
                _services.Single<IStaticDataService>(), 
                _services.Single<IWindowService>()));
            
            _services.RegisterSingle<IPlayerProgressService>(new PlayerProgressService());
            _services.RegisterSingle<ISaveLoadService>
            (new SaveLoadService(_services.Single<IPlayerProgressService>(), 
                _services.Single<IGameFactory>()));
            
            _services.RegisterSingle<IInputService>(InputService());
            RegisterAudioService();
            _services.RegisterSingle<IAdsService>(new AdsService(_services.Single<IAudioService>()));
            _services.RegisterSingle<ISceneChangerService>(new SceneChangerService(_stateMachine, _sceneLoader));
        }

        private static IInputService InputService()
        {
            if (GP_Device.IsDesktop()) return new DesktopInputService();
            return new MobileInputService();
        }

        private void RegisterAudioService()
        {
            AudioComponent musicComponent = _services.Single<IAssets>()
                .Instantiate(AssetPath.MusicPrefab).GetComponent<AudioComponent>();
            
            AudioComponent sfxComponent = _services.Single<IAssets>()
                .Instantiate(AssetPath.SfxPrefab).GetComponent<AudioComponent>();

            _services.RegisterSingle<IAudioService>(new AudioService(musicComponent, sfxComponent));
        }
        
        
    }
}
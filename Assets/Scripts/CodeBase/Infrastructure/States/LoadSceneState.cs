using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.LoadingScreen;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic.Background;
using CodeBase.UI;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerProgressService _progressService;
        private readonly IUIFactory _uiFactory;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPlayerProgressService progressService, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            Game.Player = _gameFactory.CreatePlayer(new Vector2(0, -3.5f)).GetComponent<Player>();
            Game.Hud = _gameFactory.CreateHud().GetComponent<Hud>();
            Game.Background = _gameFactory.CreatePrefabUnregistered(AssetPath.BackgroundPath, Vector2.zero)
                .GetComponent<BackgroundLoop>();
            
            _gameFactory.CreatePrefabUnregistered(AssetPath.PacificSpawnerPath, Vector2.zero);
            
            // Загрузка объектов после загрузки сцены
            // GameObject something = _gameFactory.CreateSomething(Vector3.zero);


            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}
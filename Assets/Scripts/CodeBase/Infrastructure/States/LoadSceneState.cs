﻿using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.LoadingScreen;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic;
using CodeBase.Logic.Background;
using CodeBase.StaticData.Biomes;
using CodeBase.UI;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private const string EmptyScene = "Empty";
        
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
            _sceneLoader.Load(EmptyScene, onLoaded: delegate { OnLevelLoaded(sceneName); });
        }

        private void OnLevelLoaded(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            Game.Pause();
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateInstructionsScreen();
            if (Game.IsFirstLaunch)
            {
                _uiFactory.CreateInitialScreen();
            }
            Game.Player = _gameFactory.CreatePlayer(new Vector2(0, -3.5f)).GetComponent<Player>();
            Game.HudControler = _gameFactory.CreateHud().GetComponent<HudControler>();
            _gameFactory.CreatePrefabUnregistered(AssetPath.PacificBackground, Vector2.zero);
            _gameFactory.CreatePrefabUnregistered(AssetPath.MistyBackground, Vector2.zero);
            _gameFactory.CreatePrefabUnregistered(AssetPath.DarkBackground, Vector2.zero);
            _gameFactory.CreatePrefabUnregistered(AssetPath.SpaceBackground, Vector2.zero);
            
            var pacificSpawner = _gameFactory.CreatePrefabUnregistered(AssetPath.PacificSpawner, Vector2.zero)
                .GetComponent<ObstacleSpawner>();

            var gameController =  _gameFactory.CreatePrefabUnregistered(AssetPath.GameController, Vector2.zero)
                .GetComponent<GameController>();

            gameController.Spawner = pacificSpawner;

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
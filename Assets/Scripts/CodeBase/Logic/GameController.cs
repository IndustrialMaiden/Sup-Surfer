using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Background;
using CodeBase.Utils.Disposables;
using GamePush;
using UnityEngine;

namespace CodeBase.Logic
{
    public class GameController : MonoBehaviour
    {
        private const string PacificBack = "PacificBack";
        private const string MistyBack = "MistyBack";
        private const string DarkBack = "DarkBack";
        
        public ObstacleSpawner Spawner;
        
        private Player _player;

        private IGameFactory _gameFactory;

        private CompositeDisposables _trash = new CompositeDisposables();


        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _player = Game.Player;
            _trash.Retain(_player.SubscribeOnCollision(OnPLayerStopped));
            _trash.Retain(_player.SubscribeOnPhase2(OnPhase2));
            _trash.Retain(_player.SubscribeOnPhase3(OnPhase3));
            _trash.Retain(_player.SubscribeOnPhase4(OnPhase4));
        }

        private void OnPhase2()
        {
            var pacificBack = GameObject.FindGameObjectsWithTag(PacificBack);
            foreach (var back in pacificBack)
            {
                back.GetComponent<BackgroundDestroy>().Destroy();
            }
            Destroy(Spawner.gameObject);
            Spawner = _gameFactory.CreatePrefabUnregistered(AssetPath.MistySpawner, Vector2.zero)
                .GetComponent<ObstacleSpawner>();
        }
        private void OnPhase3()
        {
            var mistyBack = GameObject.FindGameObjectsWithTag(MistyBack);
            foreach (var back in mistyBack)
            {
                back.GetComponent<BackgroundDestroy>().Destroy();
            }
            Destroy(Spawner.gameObject);
            Spawner = _gameFactory.CreatePrefabUnregistered(AssetPath.DarkSpawner, Vector2.zero)
                .GetComponent<ObstacleSpawner>();
        }
        private void OnPhase4()
        {
            var darkBack = GameObject.FindGameObjectsWithTag(DarkBack);
            foreach (var back in darkBack)
            {
                back.GetComponent<BackgroundDestroy>().Destroy();
            }
            Destroy(Spawner.gameObject);
            Spawner = _gameFactory.CreatePrefabUnregistered(AssetPath.SpaceSpawner, Vector2.zero)
                .GetComponent<ObstacleSpawner>();
        }

        private void Start()
        {
            
        }

        private void OnPLayerStopped()
        {
            Game.Pause();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
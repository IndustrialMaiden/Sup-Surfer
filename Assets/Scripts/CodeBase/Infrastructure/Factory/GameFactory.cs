using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.StaticData.Obstacles;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEditor.Experimental;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly IAssets _assets;

        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;

        public GameFactory(IAssets assets, IStaticDataService staticData, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _windowService = windowService;
        }

        
        public GameObject CreatePlayer(Vector2 at) => InstantiateRegistered(prefabPath: AssetPath.Player, at);

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            foreach (OpenWindowButton openButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openButton.Construct(_windowService);
            }

            return hud;
        }

        public GameObject CreatePrefabAtPosition(GameObject prefab, Vector2 at) => InstantiateRegistered(prefab, at);
        public GameObject CreatePrefabUnregistered(GameObject prefab, Vector2 at)
        {
            GameObject gameObject = _assets.Instantiate(prefab, at);
            return gameObject;
        }

        public GameObject CreatePrefabWithParent(GameObject prefab, Transform parent)
        {
            GameObject gameObject = _assets.Instantiate(prefab, parent);
            return gameObject;
        }

        public GameObject CreateObstacle(ObstacleDef obstacleDef, Vector2 at)
        {
            GameObject obstacle = InstantiateRegistered(obstacleDef.Prefab, at);
            
            var component = obstacle.GetComponent<ObstacleComponent>();
            component.CanMoving = obstacleDef.CanMoving;
            component.MovingSpeed = obstacleDef.MovingSpeed;

            return obstacle;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        /// <summary>
        /// Проход по компонентам объекта на наличии Прогресс Ридера или Райтера
        /// </summary>
        /// <param name="gameObject"></param>
        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponents<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        /// <summary>
        /// Регистрация ридеров и райтеров и добавление в списки
        /// </summary>
        /// <param name="progressReader"></param>
        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }
        
        /// <summary>
        /// Истанциирование зарегистрированного объекта
        /// </summary>
        /// <param name="prefabPath"></param>
        /// <returns></returns>
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        /// <summary>
        /// Истанциирование зарегистрированного объекта
        /// </summary>
        /// <param name="prefabPath"></param>
        /// <param name="at"></param>
        /// <returns></returns>
        private GameObject InstantiateRegistered(string prefabPath, Vector2 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        /// <summary>
        /// Истанциирование зарегистрированного объекта
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="at"></param>
        /// <returns></returns>
        private GameObject InstantiateRegistered(GameObject prefab, Vector2 at)
        {
            GameObject gameObject = _assets.Instantiate(prefab, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
    }
}
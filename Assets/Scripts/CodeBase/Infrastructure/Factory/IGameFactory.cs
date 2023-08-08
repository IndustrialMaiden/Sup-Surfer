using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.StaticData;
using CodeBase.StaticData.Obstacles;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        /// <summary>
        /// Метод для создания игрока
        /// </summary>
        /// <param name="at">Координаты Vector2</param>
        /// <returns></returns>
        GameObject CreatePlayer(Vector2 at);

        /// <summary>
        /// Метод, создающий префабы на позиции
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="at"></param>
        /// <returns></returns>
        GameObject CreateHud();
        GameObject CreatePrefabAtPosition(GameObject prefab, Vector2 at);
        GameObject CreatePrefabUnregistered(GameObject prefab, Vector2 at);
        GameObject CreatePrefabUnregistered(string path, Vector2 at);
        GameObject CreatePrefabWithParent(GameObject prefab, Transform parent);
        /// <summary>
        /// Метод, создающий объекты по ID, после чего получает компонент управления и конструирует объект параметрами статик даты
        /// </summary>
        /// <param name="obstacleId"></param>
        /// <param name="at"></param>
        /// <returns></returns>
        GameObject CreateObstacle(ObstacleDef obstacleDef, Vector2 at, Transform parent);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        /// <summary>
        /// Очищение списков сохраняющих и читающих прогресс
        /// </summary>
        void CleanUp();
    }
}
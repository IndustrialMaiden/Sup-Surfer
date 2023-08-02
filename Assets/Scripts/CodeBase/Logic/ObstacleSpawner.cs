using System;
using System.Collections;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.StaticData.Biomes;
using CodeBase.StaticData.Obstacles;
using CodeBase.StaticData.Repostitories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private BiomeType _biomeType;

        [Space][Header("Spawn Areas")]
        [SerializeField] private GameObject _leftSideArea;
        [SerializeField] private GameObject _rightSideArea;
        [SerializeField] private GameObject _upSideArea;
        [SerializeField] private GameObject _centralArea;

        private Vector2 _leftSideNegative;
        private Vector2 _leftSidePositive;
        
        private Vector2 _rightSideNegative;
        private Vector2 _rightSidePositive;
        
        private Vector2 _upSideNegative;
        private Vector2 _upSidePositive;
        
        private Vector2 _centralNegative;
        private Vector2 _centralPositive;

        private DefRepository<ObstacleDef> _biome;
        private SpawnProbability _spawnProbability;
        private IGameFactory _factory;

        private void Awake()
        {
            _biome = AllServices.Container.Single<IStaticDataService>().GetBiome(_biomeType);
            _spawnProbability = new SpawnProbability(_biome);
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void Start()
        {
            CalculateAreas();

            for (int i = 0; i < 10; i++)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            ObstacleDef obstacleDef = _spawnProbability.GetRandomObstacleDef();
            Vector2 randomPos = RandomizeSpawnPosition(obstacleDef.SpawnArea);
            GameObject obstacleObj = _factory.CreateObstacle(obstacleDef, randomPos);
        }

        private void CalculateAreas()
        {
            var leftSideBounds = _leftSideArea.GetComponent<Renderer>().bounds;
            var leftSidePosition = _leftSideArea.transform.position;
            _leftSideNegative.x = leftSidePosition.x - leftSideBounds.size.x / 2;
            _leftSideNegative.y = leftSidePosition.y - leftSideBounds.size.y / 2;
            _leftSidePositive.x = leftSidePosition.x + leftSideBounds.size.x / 2;
            _leftSidePositive.y = leftSidePosition.y + leftSideBounds.size.y / 2;
            
            var rightSideBounds = _rightSideArea.GetComponent<Renderer>().bounds;
            var rightSidePosition = _rightSideArea.transform.position;
            _rightSideNegative.x = rightSidePosition.x - rightSideBounds.size.x / 2;
            _rightSideNegative.y = rightSidePosition.y - rightSideBounds.size.y / 2;
            _rightSidePositive.x = rightSidePosition.x + rightSideBounds.size.x / 2;
            _rightSidePositive.y = rightSidePosition.y + rightSideBounds.size.y / 2;
            
            var upSideBounds = _upSideArea.GetComponent<Renderer>().bounds;
            var upSidePosition = _upSideArea.transform.position;
            _upSideNegative.x = upSidePosition.x - upSideBounds.size.x / 2;
            _upSideNegative.y = upSidePosition.y - upSideBounds.size.y / 2;
            _upSidePositive.x = upSidePosition.x + upSideBounds.size.x / 2;
            _upSidePositive.y = upSidePosition.y + upSideBounds.size.y / 2;
            
            var centralBounds = _centralArea.GetComponent<Renderer>().bounds;
            var centralPosition = _centralArea.transform.position;
            _centralNegative.x = centralPosition.x - centralBounds.size.x / 2;
            _centralNegative.y = centralPosition.y - centralBounds.size.y / 2;
            _centralPositive.x = centralPosition.x + centralBounds.size.x / 2;
            _centralPositive.y = centralPosition.y + centralBounds.size.y / 2;
        }

        private Vector2 RandomizeSpawnPosition(SpawnArea area)
        {
            switch (area)
            {
                case SpawnArea.LeftSide:
                    return new Vector2(
                        Random.Range(_leftSideNegative.x, _leftSidePositive.x),
                        Random.Range(_leftSideNegative.y, _leftSidePositive.y));
                case SpawnArea.RightSide:
                    return new Vector2(
                        Random.Range(_rightSideNegative.x, _rightSidePositive.x),
                        Random.Range(_rightSideNegative.y, _rightSidePositive.y));
                case SpawnArea.UpSide:
                    return new Vector2(
                        Random.Range(_upSideNegative.x, _upSidePositive.x),
                        Random.Range(_upSideNegative.y, _upSidePositive.y));
                case SpawnArea.Central:
                    return new Vector2(
                        Random.Range(_centralNegative.x, _centralPositive.x),
                        Random.Range(_centralNegative.y, _centralPositive.y));
                default:
                    throw new ArgumentOutOfRangeException(nameof(area), area, "Wrong Spawn Area Type");
            }
        }
    }


    public enum SpawnArea
    {
        LeftSide,
        RightSide,
        UpSide,
        Central
    }
}
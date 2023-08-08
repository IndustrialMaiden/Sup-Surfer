using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData.Obstacles
{
    [Serializable] [CreateAssetMenu(fileName = "ObstacleDef", menuName = "Defs/Obstacle")]
    public class ObstacleDef : ScriptableObject, IObstacleDefWithProbability
    {
        [SerializeField] private ObstacleTypeId _typeId;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private SpawnArea _spawnArea;
        [SerializeField] private bool _canMoving;
        [SerializeField] private MovementType _movementType;
        [SerializeField] private float _movingSpeed;
        [SerializeField] [Range(0f, 100f)] private float _probability;
        private double _weight;

        public ObstacleTypeId TypeId => _typeId;
        public GameObject Prefab => _prefab;

        public SpawnArea SpawnArea => _spawnArea;
        public bool CanMoving => _canMoving;
        public MovementType MovementType => _movementType;

        public float MovingSpeed => _canMoving ? _movingSpeed : 0f;
        public float Probability => _probability;
        
        public double Weight
        {
            get => _weight;
            set => _weight = value;
        }
    }
}
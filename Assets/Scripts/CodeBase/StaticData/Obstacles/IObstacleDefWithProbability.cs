using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData.Obstacles
{
    public interface IObstacleDefWithProbability
    {
        public ObstacleTypeId TypeId { get; }
        public GameObject Prefab { get; }
        
        public SpawnArea SpawnArea { get; }
        public bool CanMoving { get; }
        public float MovingSpeed { get; }
        public float Probability { get; }
        public double Weight { get; set; }
    }
}
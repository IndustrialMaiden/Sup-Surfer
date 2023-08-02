using System.Collections.Generic;
using CodeBase.StaticData.Biomes;
using CodeBase.StaticData.Obstacles;
using UnityEngine;

namespace CodeBase.StaticData.Repostitories
{
    public class DefRepository<TDefType> : ScriptableObject, IRepository<TDefType> where TDefType : IObstacleDefWithProbability
    {
        [field: SerializeField] public BiomeType BiomeType { get; private set; }
        [field: SerializeField] protected TDefType[] _collection { get; private set; }

        public TDefType[] Collection => new List<TDefType>(_collection).ToArray();
    }

    public interface IRepository<TDefType>
    {
        BiomeType BiomeType { get; }
        TDefType[] Collection { get; }
    }
}
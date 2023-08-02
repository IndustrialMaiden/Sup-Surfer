using CodeBase.StaticData;
using CodeBase.StaticData.Obstacles;
using CodeBase.StaticData.Repostitories;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class SpawnProbability
    {
        private IRepository<ObstacleDef> _repository;

        private double _accumulatedWeights;
        private System.Random _random = new System.Random();

        public SpawnProbability(IRepository<ObstacleDef> repository)
        {
            _repository = repository;
            
            CalculateWeights();
        }

        public ObstacleDef GetRandomObstacleDef()
        {
            double r = _random.NextDouble() * _accumulatedWeights;

            for (int i = 0; i < _repository.Collection.Length; i++)
            {
                if (_repository.Collection[i].Weight >= r)
                    return _repository.Collection[i];
            }
            
            return _repository.Collection[0];
        }

        private void CalculateWeights()
        {
            _accumulatedWeights = 0f;
            foreach (var prefabDef in _repository.Collection)
            {
                _accumulatedWeights += prefabDef.Probability;
                prefabDef.Weight = _accumulatedWeights;
            }
        }
    }
}
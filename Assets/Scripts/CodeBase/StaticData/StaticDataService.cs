using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData.Biomes;
using CodeBase.StaticData.Obstacles;
using CodeBase.StaticData.Repostitories;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<BiomeType, DefRepository<ObstacleDef>> _biomes;
        private Dictionary<ObstacleTypeId, ObstacleDef> _obstacles;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public StaticDataService()
        {
            LoadWindows();
            LoadBiomes();
            LoadObstacles();
        }

        private void LoadBiomes()
        {
            _biomes = Resources.LoadAll<DefRepository<ObstacleDef>>(AssetPath.BiomesPath)
                .ToDictionary(x => x.BiomeType, x=> x);
        }

        private void LoadObstacles()
        {
            _obstacles = Resources.LoadAll<ObstacleDef>(AssetPath.ObstaclesPath)
                .ToDictionary(x => x.TypeId, x=> x);
        }

        private void LoadWindows()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(AssetPath.WindowStaticData)
                .Configs.ToDictionary(x => x.WindowId, x => x);
        }
        
        

        public DefRepository<ObstacleDef> GetBiome(BiomeType biomeType)
        {
            if (_biomes.TryGetValue(biomeType, out DefRepository<ObstacleDef> biome))
                return biome;

            throw new ArgumentException("Can't find Biome with this id", biomeType.ToString());
        }

        public ObstacleDef GetObstacle(ObstacleTypeId typeId)
        {
            if (_obstacles.TryGetValue(typeId, out ObstacleDef obstacleDef))
                return obstacleDef;

            throw new ArgumentException("Can't find Obstacle with this id", typeId.ToString());
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            if (_windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig))
                return windowConfig;

            throw new ArgumentException("Can't find Window with this id", windowId.ToString());
        }
    }
}
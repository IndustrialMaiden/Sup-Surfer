using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Biomes;
using CodeBase.StaticData.Obstacles;
using CodeBase.StaticData.Repostitories;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        /// <summary>
        /// Метод получения чего-либо по ID - ключ к словарю, возвращает объект
        /// </summary>
        /// <param name="biomeType"></param>
        /// <returns></returns>
        DefRepository<ObstacleDef> GetBiome(BiomeType biomeType);

        /// <summary>
        /// Метод получения чего-либо по ID - ключ к словарю, возвращает объект
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        ObstacleDef GetObstacle(ObstacleTypeId typeId);

        WindowConfig ForWindow(WindowId windowId);
    }
}
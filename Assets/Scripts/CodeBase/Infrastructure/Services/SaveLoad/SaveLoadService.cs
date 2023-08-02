using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Utils;
using GamePush;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPlayerProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IPlayerProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }

            GP_Player.Set(ProgressKey, _progressService.Progress.ToJson());
            GP_Player.SetScore(_progressService.Progress.Score);
            GP_Player.Sync();
        }

        public PlayerProgress LoadProgress()
        {
            var progress = GP_Player.GetString(ProgressKey);

            return !string.IsNullOrEmpty(progress) ? progress.ToDeserialized<PlayerProgress>() : null;
        }
    }
}
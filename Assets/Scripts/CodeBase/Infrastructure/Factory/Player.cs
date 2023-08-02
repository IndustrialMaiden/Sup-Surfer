using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Utils.Properties;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class Player : MonoBehaviour, ISavedProgress
    {
        public IntProperty Score = new IntProperty();
        public IntProperty Credits = new IntProperty();
        
        public void LoadProgress(PlayerProgress progress)
        {
            Score.Value = progress.Score;
            Credits.Value = progress.Credits;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Score = Score.Value;
            progress.Credits = Credits.Value;
        }
    }
}
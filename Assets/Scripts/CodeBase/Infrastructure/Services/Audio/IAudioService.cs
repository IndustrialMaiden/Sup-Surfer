using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
    public interface IAudioService : IService
    {
        public void PlayMusic(string id);
        public void PlayOneShotSfx(string id);
        public void PlayOneShotSfx(AudioClip clip);
        public void PauseMusic();
        public void ResumeMusic();
        public void MuteAll();
        public void UnmuteAll();

    }
}
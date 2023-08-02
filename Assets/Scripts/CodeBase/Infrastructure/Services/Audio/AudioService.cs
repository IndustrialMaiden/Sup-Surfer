using CodeBase.Components;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
    
    public class AudioService : IAudioService
    {
        private readonly AudioComponent _music;
        private readonly AudioComponent _sfx;

        public AudioService(AudioComponent music, AudioComponent sfx)
        {
            _music = music;
            _music.Source.playOnAwake = true;
            _sfx = sfx;
        }

        public void PlayMusic(string id)
        {
            foreach (var audioData in _music.Sounds)
            {
                if (audioData.Id != id) continue;
                
                _music.Source.clip = audioData.Clip;
                _music.Source.Play();
                break;
            }
        }

        public void PlayOneShotSfx(string id)
        {
            foreach (var audioData in _sfx.Sounds)
            {
                if (audioData.Id != id) continue;
                
                _sfx.Source.PlayOneShot(audioData.Clip);
                break;
            }
        }

        public void PlayOneShotSfx(AudioClip clip)
        {
            _sfx.Source.PlayOneShot(clip);
        }

        public void PauseMusic()
        {
            _music.Source.Pause();
        }

        public void ResumeMusic()
        {
            _music.Source.UnPause();
        }

        public void MuteAll()
        {
            _music.Source.mute = true;
            _sfx.Source.mute = true;
        }

        public void UnmuteAll()
        {
            _music.Source.mute = false;
            _sfx.Source.mute = false;
        }
    }
}
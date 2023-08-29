using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class InitialWindow : WindowBase
    {
        private IAudioService _audioService;
        
        protected override void OnAwake()
        {
            _audioService = AllServices.Container.Single<IAudioService>();
            CloseButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            _audioService.PlayOneShotSfx("");
            _audioService.PlayMusic("");
            Destroy(gameObject);
        }

        protected override void OnDestroy()
        {
            CloseButton.onClick.RemoveAllListeners();
        }
    }
}
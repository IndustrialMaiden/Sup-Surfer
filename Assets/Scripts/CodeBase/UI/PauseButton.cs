using System;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(delegate { AllServices.Container.Single<IUIFactory>().CreatePauseMenu(); });
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
        }
    }
}
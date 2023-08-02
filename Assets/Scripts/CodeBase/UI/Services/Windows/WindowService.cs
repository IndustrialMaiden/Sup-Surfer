using System;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    Debug.LogWarning($"Can't find window {windowId}");
                    break;
                case WindowId.Pause:
                    _uiFactory.CreatePauseMenu();
                    break;
                case WindowId.Finish:
                    _uiFactory.CreateFinishMenu();
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowId), windowId, null);
            }
        }
    }
}
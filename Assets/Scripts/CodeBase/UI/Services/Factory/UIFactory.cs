using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPlayerProgressService _playerProgress;

        private GameObject _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData, IPlayerProgressService playerProgress)
        {
            _assets = assets;
            _staticData = staticData;
            _playerProgress = playerProgress;
        }
        
        public void CreateUIRoot() => _uiRoot = _assets.Instantiate(AssetPath.UIRoot);

        public void CreateInitialScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Initial);
            Object.Instantiate(config.Prefab, _uiRoot.transform).Construct(_playerProgress);
        }
        
        public void CreateInstructionsScreen()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Instructions);
            Object.Instantiate(config.Prefab, _uiRoot.transform).Construct(_playerProgress);
        }

        public void CreatePauseMenu()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Pause);
            Object.Instantiate(config.Prefab, _uiRoot.transform).Construct(_playerProgress);
        }

        public void CreateFinishMenu()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Finish);
            Object.Instantiate(config.Prefab, _uiRoot.transform).Construct(_playerProgress);
        }
    }
}
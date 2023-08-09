/*using CodeBase.Infrastructure;
using CodeBase.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    class ShopWindow : WindowBase
    {
        [SerializeField] private Text _creditsText;

        private CompositeDisposables _trash;

        protected override void Initialize() => RefreshCreditsText(0,0);

        protected override void SubscribeUpdates()
        {
            _trash.Retain(Game.Player.Credits.Subscribe(RefreshCreditsText));
        }

        private void RefreshCreditsText(int oldValue, int newValue)
        {
            Game.Player.UpdateProgress(Progress);
            _creditsText.text = Progress.Credits.ToString();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _trash.Dispose();
        }
    }
}*/
using CodeBase.Infrastructure;

namespace CodeBase.UI.Windows
{
    public class InstructionScreen : WindowBase
    {
        protected override void OnAwake()
        {
            CloseButton.onClick.AddListener(OnPlayerClick);
        }

        private void OnPlayerClick()
        {
            Game.Resume();
            Destroy(gameObject);
        }

        protected override void OnDestroy()
        {
            CloseButton.onClick.RemoveAllListeners();
        }
    }
}
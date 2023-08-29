using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        void CreatePauseMenu();
        void CreateFinishMenu();
        void CreateInitialScreen();
        void CreateInstructionsScreen();
    }
}
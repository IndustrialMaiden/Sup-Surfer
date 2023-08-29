namespace CodeBase.Infrastructure.Services.SceneChanger
{
    public interface ISceneChangerService : IService
    {
        void Enter(string sceneName);

    }
}
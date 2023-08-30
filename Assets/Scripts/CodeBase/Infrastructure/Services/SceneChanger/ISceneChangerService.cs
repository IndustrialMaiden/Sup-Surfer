namespace CodeBase.Infrastructure.Services.SceneChanger
{
    public interface ISceneChangerService : IService
    {
        void LoadScene(string sceneName);

    }
}
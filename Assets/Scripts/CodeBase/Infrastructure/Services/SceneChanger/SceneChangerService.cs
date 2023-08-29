using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Services.SceneChanger
{
    public class SceneChangerService : ISceneChangerService
    {
        private const string EmptyScene = "Empty";
        private SceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        
        public SceneChangerService(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter(string sceneName)
        {
            _stateMachine.Enter<LoadSceneState, string>(EmptyScene);
            _stateMachine.Enter<LoadSceneState, string>(sceneName);
        }
    }
}
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Services.SceneChanger
{
    public class SceneChangerService : ISceneChangerService
    {
        private GameStateMachine _stateMachine;
        
        public SceneChangerService(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void LoadScene(string sceneName)
        {
            _stateMachine.Enter<LoadSceneState, string>(sceneName);
        }
    }
}
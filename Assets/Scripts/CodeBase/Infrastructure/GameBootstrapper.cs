using CodeBase.Infrastructure.LoadingScreen;
using CodeBase.Infrastructure.States;
using GamePush;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        [SerializeField] private LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
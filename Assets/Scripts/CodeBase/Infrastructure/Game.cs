using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.LoadingScreen;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static Player Player;
        public static Hud Hud;
        
        public readonly GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}
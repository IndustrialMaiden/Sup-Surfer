using System;
using System.Collections.Generic;
using CodeBase.Components.Shop;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "Main";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPlayerProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadSceneState, string>(MainScene);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            if (_saveLoadService.LoadProgress() == null)
            {
                _progressService.Progress = NewProgress();
                _saveLoadService.SaveProgress();
            }
            else
            {
                _progressService.Progress = _saveLoadService.LoadProgress();
            }
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress()
            {
                Score = 0,
                RewardCount = 1,
                Credits = 0,
                LastClaimedTime = new DateTime(2020, 01, 01),
                BoolPurchases = new List<BoolPurchase>(DefsFacade.Instance.Purchases.BoolPurchases),
                IntPurchases = new List<IntPurchase>(DefsFacade.Instance.Purchases.IntPurchases),
            };
        }
    }
}
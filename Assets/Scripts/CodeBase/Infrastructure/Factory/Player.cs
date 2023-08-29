using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Utils.Disposables;
using CodeBase.Utils.Properties;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class Player : MonoBehaviour, ISavedProgress
    {
        private readonly string ObstacleTag = "Obstacle";
        
        public IntProperty Score = new IntProperty();
        public bool IsAlive { get; set; } = true;
        private Action _enterObstacleAction;

        private Action _phase2;
        private Action _phase3;
        private Action _phase4;

        private ISaveLoadService _saveLoadService;


        public IDisposable SubscribeOnCollision(Action call)
        {
            _enterObstacleAction += call;
            return new ActionDisposables(() => _enterObstacleAction -= call);
        }
        public IDisposable SubscribeOnPhase2(Action call)
        {
            _phase2 += call;
            return new ActionDisposables(() => _phase2 -= call);
        }
        public IDisposable SubscribeOnPhase3(Action call)
        {
            _phase3 += call;
            return new ActionDisposables(() => _phase3 -= call);
        }
        public IDisposable SubscribeOnPhase4(Action call)
        {
            _phase4 += call;
            return new ActionDisposables(() => _phase4 -= call);
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void Start()
        {
            StartCoroutine(AddPoints());
            Score.Value = 0;
        }

        private IEnumerator AddPoints()
        {
            while (IsAlive)
            {
                Score.Value += 1;
                yield return new WaitForSeconds(0.1f);

                if (Score.Value == 50)
                {
                    _phase2?.Invoke();
                }

                if (Score.Value == 100)
                {
                    _phase3?.Invoke();
                }

                if (Score.Value == 150)
                {
                    _phase4?.Invoke();
                }
            }
        }

        public void OnPlayerRestart()
        {
            StartCoroutine(AddPoints());
        }

        private void OnTriggerEnter2D(Collider2D obstacle)
        {
            if (obstacle.gameObject.CompareTag(ObstacleTag))
            {
                _enterObstacleAction?.Invoke();
                IsAlive = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                _saveLoadService.SaveProgress();
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Score.Value = progress.Score;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Score = Score.Value;
        }
    }
}
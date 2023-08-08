using System;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Utils.Disposables;
using CodeBase.Utils.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Factory
{
    public class Player : MonoBehaviour, ISavedProgress
    {
        private readonly string ObstacleTag = "Obstacle";
        
        public IntProperty Score = new IntProperty();
        public IntProperty Credits = new IntProperty();

        private Action _enterObstacleAction;

        private void Awake()
        {
            Score.Value = 0;
        }

        private void OnTriggerEnter2D(Collider2D obstacle)
        {
            if (obstacle.gameObject.CompareTag(ObstacleTag))
            {
                //_enterObstacleAction?.Invoke();
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("COLLISION");
            }
        }

        public IDisposable SubscribeOnCollision(Action call)
        {
            _enterObstacleAction += call;
            return new ActionDisposables(() => _enterObstacleAction -= call);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Score.Value = progress.Score;
            Credits.Value = progress.Credits;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Score = Score.Value;
            progress.Credits = Credits.Value;
        }
    }
}
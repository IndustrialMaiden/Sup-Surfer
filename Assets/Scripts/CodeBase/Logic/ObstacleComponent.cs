using System;
using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class ObstacleComponent : MonoBehaviour
    {
        public bool CanMoving;
        public MovementType MovementType;
        public float MovingSpeed;

        private Vector3 _startPosition;

        private readonly string ObstacleTag = "Obstacle";

        private void Start()
        {
            gameObject.tag = ObstacleTag;
            
            StartCoroutine(SelfDestroy());
            
            _startPosition = transform.position;
            
            
            if (!CanMoving) return;
            switch (MovementType)
            {
                case MovementType.LeftToRight:
                    StartCoroutine(LeftToRightMovement());
                    break;
                case MovementType.RightToLeft:
                    StartCoroutine(RightToLeftMovement());
                    break;
                case MovementType.UpToDown:
                    break;
                case MovementType.UpToDownSinus:
                    StartCoroutine(UpToDownSinusMovement());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            MoveWithBackground();
        }

        private void MoveWithBackground()
        {
            var position = transform.position;
            transform.position = Vector3.MoveTowards(position,
                position + new Vector3(0, -Constants.BackgroundMovingSpeed, 0),
                Constants.BackgroundMovingSpeed * Time.deltaTime);
        }
        
        private IEnumerator LeftToRightMovement()
        {
            while (true)
            {
                var position = transform.position;
                transform.position = Vector3.MoveTowards(position,
                    position + new Vector3(MovingSpeed, 0, 0),
                    MovingSpeed * Time.deltaTime);
                yield return null;
            }
        }
        private IEnumerator RightToLeftMovement()
        {
            while (true)
            {
                var position = transform.position;
                transform.position = Vector3.MoveTowards(position,
                    position + new Vector3(-MovingSpeed, 0, 0),
                    MovingSpeed * Time.deltaTime);
                yield return null;
            }
        }
        private IEnumerator UpToDownSinusMovement()
        {
            float frequency = 10f;
            float magnitude = 0.05f;

            while (true)
            {
                transform.position += (transform.right * Mathf.Sin(Time.time * frequency) * magnitude) * Time.timeScale;
                yield return null;
            }
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
    
    public enum MovementType
    {
        LeftToRight,
        RightToLeft,
        UpToDown,
        UpToDownSinus
    }
}